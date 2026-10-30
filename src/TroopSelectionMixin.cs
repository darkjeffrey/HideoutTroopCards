using System;
using System.Collections.Generic;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TroopSelection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Party;
using TaleWorlds.Library;
using static TaleWorlds.CampaignSystem.Party.PartyScreenLogic;

namespace HideoutTroopCards
{
    /// <summary>
    /// Gives the troop selection screen the party screen's sort control (Type, Name, Count, Tier, Custom).
    /// Custom is the game's own order; flipping it lists the lowest levels first.
    /// </summary>
    [ViewModelMixin]
    public sealed class TroopSelectionMixin : BaseViewModelMixin<GameMenuTroopSelectionVM>
    {
        // Kept for the whole session so the next screen opens with the same order.
        private static TroopSortType _lastSortType = TroopSortType.Custom;
        private static bool _lastAscending;
        private static bool _hasSortedBefore;

        private readonly bool _gameOrderAscending;

        public TroopSelectionMixin(GameMenuTroopSelectionVM vm) : base(vm)
        {
            HtcShowSort = HtcSettings.Enabled && HtcSettings.ShowSortControl;
            HtcSortController = new PartySortControllerVM(PartyRosterSide.Right, OnSort);
            _gameOrderAscending = HtcSortController.IsAscending;

            if (HtcShowSort && HtcSettings.RememberSort && _hasSortedBefore)
                RunSafely("RestoreSort", RestoreLastSort);
        }

        [DataSourceProperty] public PartySortControllerVM HtcSortController { get; }
        [DataSourceProperty] public bool HtcShowSort { get; }

        private void RestoreLastSort()
        {
            HtcSortController.IsAscending = _lastAscending;
            HtcSortController.SelectSortType(_lastSortType);
            // SelectSortType only calls back when the selection changes.
            OnSort(PartyRosterSide.Right, _lastSortType, _lastAscending);
        }

        private void OnSort(PartyRosterSide side, TroopSortType sortType, bool ascending) => RunSafely("Sort", () =>
        {
            MBBindingList<TroopSelectionItemVM> troops = ViewModel?.Troops;
            if (troops == null) return;

            if (sortType == TroopSortType.Invalid) sortType = TroopSortType.Custom;
            troops.Sort(CreateComparer(sortType, ascending));

            _lastSortType = sortType;
            _lastAscending = ascending;
            _hasSortedBefore = true;
            Log.Debug($"Sorted {troops.Count} cards by {sortType}, {(ascending ? "ascending" : "descending")}.");
        });

        private static void RunSafely(string what, Action action)
        {
            try { action(); }
            catch (Exception e) { Log.Error(what, e); }
        }

        private IComparer<TroopSelectionItemVM> CreateComparer(TroopSortType sortType, bool ascending)
        {
            if (sortType != TroopSortType.Custom)
                return new CardComparer(sortType, ascending, HtcSettings.KeepHeroesOnTop);

            bool isGameOrder = ascending == _gameOrderAscending;
            return isGameOrder
                ? (IComparer<TroopSelectionItemVM>)new TroopItemComparer()
                : new CardComparer(TroopSortType.Custom, ascending: true, heroesOnTop: true);
        }

        /// <summary>You always come first; heroes next if asked; then the sort key, level and name.</summary>
        private sealed class CardComparer : IComparer<TroopSelectionItemVM>
        {
            private readonly TroopSortType _sortType;
            private readonly bool _ascending;
            private readonly bool _heroesOnTop;

            public CardComparer(TroopSortType sortType, bool ascending, bool heroesOnTop)
            {
                _sortType = sortType;
                _ascending = ascending;
                _heroesOnTop = heroesOnTop;
            }

            public int Compare(TroopSelectionItemVM x, TroopSelectionItemVM y)
            {
                CharacterObject a = x?.Troop.Character;
                CharacterObject b = y?.Troop.Character;
                if (a == null || b == null) return (a == null).CompareTo(b == null);

                int pinned = b.IsPlayerCharacter.CompareTo(a.IsPlayerCharacter);
                if (pinned == 0 && _heroesOnTop) pinned = b.IsHero.CompareTo(a.IsHero);
                if (pinned != 0) return pinned;

                int order = SortKey(x).CompareTo(SortKey(y));
                if (order == 0 && _sortType != TroopSortType.Name)
                {
                    order = a.Level.CompareTo(b.Level);
                    if (order == 0) order = string.Compare(a.Name?.ToString(), b.Name?.ToString(), StringComparison.CurrentCulture);
                }
                return _ascending ? order : -order;
            }

            private IComparable SortKey(TroopSelectionItemVM card)
            {
                CharacterObject character = card.Troop.Character;
                switch (_sortType)
                {
                    case TroopSortType.Type: return FormationOrder(character);
                    case TroopSortType.Tier: return character.Tier;
                    case TroopSortType.Count: return card.MaxAmount;
                    case TroopSortType.Custom: return character.Level;
                    default: return character.Name?.ToString() ?? "";
                }
            }

            // Infantry, ranged, cavalry, horse archers: the same split as the type icon on each card.
            private static int FormationOrder(CharacterObject character)
            {
                if (character.IsMounted) return character.IsRanged ? 3 : 2;
                return character.IsRanged ? 1 : 0;
            }
        }
    }
}

using System;
using System.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TroopSelection;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;

namespace HideoutTroopCards
{
    /// <summary>Weapon icons for one troop selection card (laid out by TroopCardIconsPatch).</summary>
    [ViewModelMixin]
    public sealed class TroopCardMixin : BaseViewModelMixin<TroopSelectionItemVM>
    {
        public TroopCardMixin(TroopSelectionItemVM vm) : base(vm)
        {
            CharacterObject character = vm.Troop.Character;
            HtcIsPlayer = character?.IsPlayerCharacter ?? false;
            if (!ShowsIcons(character)) return;
            try
            {
                foreach (StringItemWithHintVM icon in WeaponIcons.For(character)) HtcWeaponIcons.Add(icon);
            }
            catch (Exception e)
            {
                Log.Error("WeaponIcons", e);
            }
        }

        [DataSourceProperty] public MBBindingList<StringItemWithHintVM> HtcWeaponIcons { get; } = new MBBindingList<StringItemWithHintVM>();
        [DataSourceProperty] public bool HtcHasWeaponIcons => HtcWeaponIcons.Any();

        /// <summary>Your own card has a lock in the bottom right corner, so the icons make room for it.</summary>
        [DataSourceProperty] public bool HtcIsPlayer { get; }

        private static bool ShowsIcons(CharacterObject character) =>
            character != null && HtcSettings.Enabled && HtcSettings.ShowWeaponIcons
            && (character.IsHero || HtcSettings.ShowIconsForRegularTroops);
    }
}

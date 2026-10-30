using System;
using System.Linq;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Roster;

namespace HideoutTroopCards
{
    /// <summary>
    /// Auto-pick companions: before the troop selection screen opens, every healthy hero is added to the preselected
    /// crew, highest level first. Remaining slots keep the game's own picks.
    /// </summary>
    [HarmonyPatch(typeof(MenuContext), nameof(MenuContext.OpenTroopSelection))]
    internal static class AutoPickPatch
    {
        private static void Prefix(TroopRoster fullRoster, ref TroopRoster initialSelections,
            Func<CharacterObject, bool> canChangeStatusOfTroop, int maxSelectableTroopCount)
        {
            if (!HtcSettings.Enabled || !HtcSettings.AutoPickCompanions || fullRoster == null || initialSelections == null) return;
            try
            {
                bool CanChange(CharacterObject c) => canChangeStatusOfTroop?.Invoke(c) ?? true;
                var picks = new Picks(maxSelectableTroopCount);

                // Locked troops (you, quest characters) stay exactly as the game chose them.
                foreach (TroopRosterElement e in initialSelections.GetTroopRoster().Where(e => !CanChange(e.Character)))
                    picks.AddLocked(e.Character, e.Number);

                foreach (TroopRosterElement e in fullRoster.GetTroopRoster()
                             .Where(e => IsHealthyCompanion(e) && CanChange(e.Character))
                             .OrderByDescending(e => e.Character.Level))
                    picks.TryAdd(e.Character, 1);

                foreach (TroopRosterElement e in initialSelections.GetTroopRoster().Where(e => CanChange(e.Character)))
                    picks.TryAdd(e.Character, e.Number - picks.Roster.GetTroopCount(e.Character));

                initialSelections = picks.Roster;
                Log.Debug($"Auto-pick: {picks.Roster.TotalManCount}/{maxSelectableTroopCount} selected.");
            }
            catch (Exception e)
            {
                Log.Error("AutoPick", e);
            }
        }

        private static bool IsHealthyCompanion(TroopRosterElement e) =>
            e.Character.IsHero && !e.Character.IsPlayerCharacter && e.Number - e.WoundedNumber > 0;

        /// <summary>A roster that never grows past the screen's troop limit.</summary>
        private sealed class Picks
        {
            private readonly int _limit;
            public TroopRoster Roster { get; } = TroopRoster.CreateDummyTroopRoster();

            public Picks(int limit) => _limit = limit;

            public void AddLocked(CharacterObject character, int count)
            {
                if (count > 0) Roster.AddToCounts(character, count);
            }

            public void TryAdd(CharacterObject character, int count)
            {
                int fits = Math.Min(count, _limit - Roster.TotalManCount);
                if (fits > 0) Roster.AddToCounts(character, fits);
            }
        }
    }
}

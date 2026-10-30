using System;

namespace HideoutTroopCards
{
    /// <summary>
    /// Read-only view of the current settings. Values come from the MCM page (HideoutTroopCardsMcmSettings);
    /// the defaults there are used if MCM isn't loaded yet.
    /// </summary>
    public static class HtcSettings
    {
        private static readonly HideoutTroopCardsMcmSettings Defaults = new HideoutTroopCardsMcmSettings();

        private static HideoutTroopCardsMcmSettings Current
        {
            get
            {
                try { return HideoutTroopCardsMcmSettings.Instance ?? Defaults; }
                catch (Exception) { return Defaults; }
            }
        }

        public static bool Enabled => Current.Enabled;
        public static bool AutoPickCompanions => Current.AutoPickCompanions;
        public static bool ShowWeaponIcons => Current.ShowWeaponIcons;
        public static bool ShowIconsForRegularTroops => Current.ShowIconsForRegularTroops;
        public static bool ShowThrownIcons => Current.ShowThrownIcons;
        public static bool ShowAmmoIcons => Current.ShowAmmoIcons;
        public static bool ShowSortControl => Current.ShowSortControl;
        public static bool KeepHeroesOnTop => Current.KeepHeroesOnTop;
        public static bool RememberSort => Current.RememberSort;
        public static bool DebugMessages => Current.DebugMessages;
    }
}

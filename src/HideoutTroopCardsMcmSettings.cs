using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Localization;

namespace HideoutTroopCards
{
    /// <summary>The "Hideout Troop Cards" page in Mod Options (MCM). Saved to Documents\Mount and Blade II Bannerlord\Configs\ModSettings.</summary>
    public sealed class HideoutTroopCardsMcmSettings : AttributeGlobalSettings<HideoutTroopCardsMcmSettings>
    {
        public override string Id => "HideoutTroopCards";
        // MCM shows DisplayName as-is (it doesn't translate it like setting names and hints) and stores it the first
        // time it asks, so resolve it here. If the game's text system isn't ready yet, TextObject hands the raw
        // "{=id}text" back; drop the id then so the English name still shows.
        public override string DisplayName
        {
            get
            {
                string name = new TextObject("{=htc_mcm_title}Hideout Troop Cards").ToString();
                if (name.StartsWith("{=", System.StringComparison.Ordinal))
                {
                    int close = name.IndexOf('}');
                    if (close >= 0) name = name.Substring(close + 1);
                }
                return name;
            }
        }
        public override string FolderName => "HideoutTroopCards";
        public override string FormatType => "json2";

        // ---------------- General ----------------
        private const string General = "{=htc_mcm_group_general}General";

        [SettingPropertyBool("{=htc_mcm_Enabled}Enable", Order = 0, RequireRestart = false,
            HintText = "{=htc_mcm_Enabled_hint}Turn the mod on or off.")]
        [SettingPropertyGroup(General, GroupOrder = 0)]
        public bool Enabled { get; set; } = true;

        [SettingPropertyBool("{=htc_mcm_AutoPickCompanions}Auto-pick companions", Order = 1, RequireRestart = false,
            HintText = "{=htc_mcm_AutoPickCompanions_hint}When the troop selection screen opens, every healthy hero in your party is already selected, highest level first. The remaining slots keep the game's own picks.")]
        [SettingPropertyGroup(General, GroupOrder = 0)]
        public bool AutoPickCompanions { get; set; } = false;

        // ---------------- Cards ----------------
        private const string Cards = "{=htc_mcm_group_cards}Troop cards";

        [SettingPropertyBool("{=htc_mcm_ShowWeaponIcons}Show weapon icons", Order = 0, RequireRestart = false,
            HintText = "{=htc_mcm_ShowWeaponIcons_hint}Show an icon for each weapon a hero has equipped on their card. Hover an icon to see the item's name.")]
        [SettingPropertyGroup(Cards, GroupOrder = 1)]
        public bool ShowWeaponIcons { get; set; } = true;

        [SettingPropertyBool("{=htc_mcm_ShowIconsForRegularTroops}Weapon icons for regular troops", Order = 1, RequireRestart = false,
            HintText = "{=htc_mcm_ShowIconsForRegularTroops_hint}Also show weapon icons on regular troop cards. These show the troop type's first equipment set, as the party screen does.")]
        [SettingPropertyGroup(Cards, GroupOrder = 1)]
        public bool ShowIconsForRegularTroops { get; set; } = false;

        [SettingPropertyBool("{=htc_mcm_ShowThrownIcons}Show thrown weapons", Order = 2, RequireRestart = false,
            HintText = "{=htc_mcm_ShowThrownIcons_hint}Show an icon for javelins, throwing axes and other thrown weapons.")]
        [SettingPropertyGroup(Cards, GroupOrder = 1)]
        public bool ShowThrownIcons { get; set; } = false;

        [SettingPropertyBool("{=htc_mcm_ShowAmmoIcons}Show quivers", Order = 3, RequireRestart = false,
            HintText = "{=htc_mcm_ShowAmmoIcons_hint}Show an icon for arrows, bolts and sling stones.")]
        [SettingPropertyGroup(Cards, GroupOrder = 1)]
        public bool ShowAmmoIcons { get; set; } = false;

        // ---------------- Sorting ----------------
        private const string Sorting = "{=htc_mcm_group_sorting}Sorting";

        [SettingPropertyBool("{=htc_mcm_ShowSortControl}Show sort control", Order = 0, RequireRestart = false,
            HintText = "{=htc_mcm_ShowSortControl_hint}Add the party screen's sort control (Type, Name, Count, Tier, Custom) to the troop selection screen. Custom is the game's own order.")]
        [SettingPropertyGroup(Sorting, GroupOrder = 2)]
        public bool ShowSortControl { get; set; } = true;

        [SettingPropertyBool("{=htc_mcm_KeepHeroesOnTop}Keep heroes on top", Order = 1, RequireRestart = false,
            HintText = "{=htc_mcm_KeepHeroesOnTop_hint}When sorting, keep you and your heroes before the regular troops.")]
        [SettingPropertyGroup(Sorting, GroupOrder = 2)]
        public bool KeepHeroesOnTop { get; set; } = true;

        [SettingPropertyBool("{=htc_mcm_RememberSort}Remember sort", Order = 2, RequireRestart = false,
            HintText = "{=htc_mcm_RememberSort_hint}Open the screen with the last sort you picked. Resets when you restart the game.")]
        [SettingPropertyGroup(Sorting, GroupOrder = 2)]
        public bool RememberSort { get; set; } = true;

        // ---------------- Testing ----------------
        private const string Testing = "{=htc_mcm_group_testing}Testing";

        [SettingPropertyBool("{=htc_mcm_DebugMessages}Debug messages", Order = 0, RequireRestart = false,
            HintText = "{=htc_mcm_DebugMessages_hint}Show chat messages about what the mod is doing. Useful for checking the mod works; turn it off for normal play.")]
        [SettingPropertyGroup(Testing, GroupOrder = 3)]
        public bool DebugMessages { get; set; } = false;
    }
}

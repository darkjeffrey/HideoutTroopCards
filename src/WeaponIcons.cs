using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using ItemType = TaleWorlds.Core.ItemObject.ItemTypeEnum;

namespace HideoutTroopCards
{
    /// <summary>One icon per kind of weapon a character carries. Text is the sprite name, the hint is the item name.</summary>
    internal static class WeaponIcons
    {
        private const string SpritePrefix = "General\\EquipmentIcons\\equipment_type_";

        public static IEnumerable<StringItemWithHintVM> For(CharacterObject character)
        {
            var shownSprites = new HashSet<string>();
            foreach (ItemObject weapon in EquippedWeapons(character))
            {
                if (!IsShown(weapon.ItemType)) continue;
                string sprite = SpritePrefix + SpriteSuffix(weapon.ItemType);
                if (shownSprites.Add(sprite)) yield return new StringItemWithHintVM(sprite, weapon.Name);
            }
        }

        // Heroes use their own gear; regular troops show their first battle set, like the party screen.
        private static IEnumerable<ItemObject> EquippedWeapons(CharacterObject character)
        {
            Equipment equipment = character.IsHero ? character.HeroObject?.BattleEquipment : character.FirstBattleEquipment;
            if (equipment == null) yield break;

            for (EquipmentIndex slot = EquipmentIndex.Weapon0; slot <= EquipmentIndex.Weapon3; slot++)
            {
                EquipmentElement element = equipment[slot];
                if (!element.IsEmpty && element.Item != null) yield return element.Item;
            }
        }

        private static bool IsShown(ItemType type)
        {
            switch (type)
            {
                case ItemType.Banner: return false;
                case ItemType.Thrown: return HtcSettings.ShowThrownIcons;
                case ItemType.Arrows:
                case ItemType.Bolts:
                case ItemType.SlingStones: return HtcSettings.ShowAmmoIcons;
                default: return true;
            }
        }

        private static string SpriteSuffix(ItemType type)
        {
            switch (type)
            {
                case ItemType.OneHandedWeapon: return "one_handed";
                case ItemType.TwoHandedWeapon: return "two_handed";
                case ItemType.Polearm: return "polearm";
                case ItemType.Bow: return "bow";
                case ItemType.Crossbow: return "crossbow";
                case ItemType.Sling: return "sling";
                case ItemType.Thrown: return "throwing";
                case ItemType.Shield: return "shield";
                case ItemType.Arrows:
                case ItemType.Bolts: return "quiver";
                case ItemType.SlingStones: return "slingstones";
                default: return "default";
            }
        }
    }
}

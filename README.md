# Hideout Troop Cards

A Mount & Blade II: Bannerlord mod (v1.4.8) that improves the troop selection screen you get before attacking a hideout (and before a lord's hall fight). Each hero's card shows icons for the weapons they have equipped, the cards can be sorted with the same sort control as the party screen, and your companions can be picked automatically.

## How it works

- **Weapon icons**: the bottom right corner of each hero's card shows one icon per kind of weapon they carry (one-handed, two-handed, polearm, bow, crossbow, sling, shield), each kind once. Thrown weapons and quivers (arrows, bolts, sling stones) have their own options and are off by default; banners are never shown. These are the game's own equipment icons. Hover an icon to see the item's name. Regular troops can show icons too (off by default); they show the troop type's first equipment set, as the party screen does.
- **Sorting**: the party screen's sort dropdown (Type, Name, Count, Tier, Custom) and an ascending/descending arrow sit in the top right corner. Custom is the game's own order (you, heroes, then highest level); its arrow flips the levels. You and your heroes stay on top unless you turn that off. The last sort is remembered until you restart the game.
- **Auto-pick companions** (off by default): when the screen opens, every healthy hero in your party is already selected, highest level first. The remaining slots keep the game's own picks. Wounded heroes can't be picked, as in the base game.

## Options (Mod Options, MCM)

| Option | Default | What it does |
|---|---|---|
| Enable | On | Turns the mod on or off. |
| Auto-pick companions | Off | Preselects every healthy hero in your party. |
| Show weapon icons | On | Weapon icons on hero cards. |
| Weapon icons for regular troops | Off | Weapon icons on regular troop cards too. |
| Show thrown weapons | Off | Icon for javelins, throwing axes and other thrown weapons. |
| Show quivers | Off | Icon for arrows, bolts and sling stones. |
| Show sort control | On | Adds the sort dropdown and order button. |
| Keep heroes on top | On | Sorting keeps you and your heroes before regular troops. |
| Remember sort | On | Reopens the screen with the last sort you picked. |
| Debug messages | Off | Shows chat messages about what the mod is doing, for testing. |

## Requirements

Bannerlord v1.4.8, Bannerlord.Harmony, ButterLib, UIExtenderEx and Mod Configuration Menu v5. Load it after them.

## Code

| File | What it does |
|---|---|
| `src/PrefabPatches.cs` | UIExtenderEx patches that add the icon strip and the sort control to the game's `GameMenuTroopSelection` prefab. |
| `src/TroopCardMixin.cs` | Gives each card its list of weapon icons. |
| `src/WeaponIcons.cs` | Picks one icon per kind of equipped weapon. |
| `src/TroopSelectionMixin.cs` | The sort control and the card comparers. |
| `src/AutoPickPatch.cs` | Harmony prefix on `MenuContext.OpenTroopSelection` that preselects your companions. |
| `Module/GUI/Brushes/HideoutTroopCards.xml` | Brushes for the icons and the order arrow. |

## Building

From the `src` folder run `dotnet build -c Release`. The build copies the dll, `SubModule.xml`, `ModuleData` and `GUI` into `Modules\HideoutTroopCards` in the game folder. If the game is installed somewhere else, pass `/p:GameFolder="..."`.

## Translations

All English text is in the code as `{=id}English text`. `Module/ModuleData/Languages/std_HideoutTroopCards.xml` lists every string and is the template for translations.

To add a language, for example German, run `python BannerlordModTools/bltools.py loc add-language HideoutTroopCards DE` from the `repos` folder. It creates `Module/ModuleData/Languages/DE/` with `language_data.xml` and a translation file where every line is marked NEW; translate each `text` attribute, keeping the ids and every `{VARIABLE}` unchanged. Running `loc update` after the code changes marks new, changed and obsolete lines.

## Support

If you enjoy the mod, you can support me on [Ko-fi](https://ko-fi.com/darkjeffrey).

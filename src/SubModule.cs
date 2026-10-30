using System;
using Bannerlord.UIExtenderEx;
using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace HideoutTroopCards
{
    public class SubModule : MBSubModuleBase
    {
        private Harmony _harmony;
        private UIExtender _extender;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                _harmony = new Harmony("darkjeffrey.hideouttroopcards");
                _harmony.PatchAll(typeof(SubModule).Assembly);
            }
            catch (Exception e)
            {
                Log.Error("Harmony", e);
            }

            try
            {
                // Weapon icons and the sort control are prefab patches plus view model mixins (PrefabPatches.cs).
                _extender = UIExtender.Create("HideoutTroopCards");
                _extender.Register(typeof(SubModule).Assembly);
                _extender.Enable();
            }
            catch (Exception e)
            {
                Log.Error("UIExtenderEx", e);
            }
        }
    }
}

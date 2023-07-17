global using FloLib.Attributes;
global using FloLib.Utils.Extensions;

using BepInEx;
using BepInEx.Unity.IL2CPP;
using FloLib.Events;
using FloLib.Utils;
using GTFO.API;
using HarmonyLib;
using System.Linq;

namespace FloLib;
[BepInPlugin("GTFO.FloLib", "FloLib", VersionInfo.Version)]
[BepInDependency("dev.gtfomodding.gtfo-api", BepInDependency.DependencyFlags.HardDependency)]
internal class EntryPoint : BasePlugin
{
    private Harmony _Harmony = null;

    public override void Load()
    {
        _Harmony = new Harmony($"{VersionInfo.RootNamespace}.Harmony");
        _Harmony.PatchAll();

        Automation.RegisterTypes(GetType());

        int c = 0;
        while (RNG.Global.Float01 != 1.0f)
        {
            c++;
        }

        Logger.Error($"Hmm {c}");
    }

    public override bool Unload()
    {
        _Harmony.UnpatchSelf();
        return base.Unload();
    }
}

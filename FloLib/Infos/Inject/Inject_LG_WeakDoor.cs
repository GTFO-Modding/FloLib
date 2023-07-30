using HarmonyLib;
using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Infos.Inject;
[HarmonyPatch(typeof(LG_WeakDoor), nameof(LG_WeakDoor.Setup))]
internal static class Inject_LG_WeakDoor
{
    static void Postfix(LG_WeakDoor __instance)
    {
        LG_Objects.WeakDoors.Add(__instance);
    }
}

using HarmonyLib;
using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Infos.Inject;
[HarmonyPatch(typeof(LG_SecurityDoor), nameof(LG_SecurityDoor.Setup))]
internal static class Inject_LG_SecDoor
{
    static void Postfix(LG_SecurityDoor __instance)
    {
        LG_Objects.SecurityDoors.Add(__instance);
    }
}

using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Events.Inject;
[HarmonyPatch(typeof(StartMainGame), nameof(StartMainGame.Awake))]
internal class Inject_StartMainGame
{
    static void Postfix()
    {
        StartGameEvent.Invoke_OnGameLoaded();
    }
}

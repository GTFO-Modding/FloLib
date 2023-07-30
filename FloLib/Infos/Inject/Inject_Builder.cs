using HarmonyLib;
using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Infos.Inject;
[HarmonyPatch(typeof(Builder), nameof(Builder.BuildDone))]
internal static class Inject_Builder
{
    [HarmonyPriority(Priority.First)]
    static void Postfix()
    {
        //LG_Objects.SecurityDoors.AddRange(Find<LG_SecurityDoor>()); Handled On Inject_LG_SecDoor
        //LG_Objects.WeakDoors.AddRange(Find<LG_WeakDoor>()); Handled On Inject_LG_WeakDoor
        //LG_Objects.Lights.AddRange(Find<LG_Light>()); Handled On Inject_LG_LightBuild
        LG_Objects.Terminals.AddRange(Find<LG_ComputerTerminal>());
        LG_Objects.LabDisplays.AddRange(Find<LG_LabDisplay>());

        LG_Objects.DoorButtons.AddRange(Find<LG_DoorButton>());
        LG_Objects.WeakLocks.AddRange(Find<LG_WeakLock>());
        LG_Objects.HSUActivators.AddRange(Find<LG_HSUActivator_Core>());
        LG_Objects.CarryItems.AddRange(Find<CarryItemPickup_Core>());
        LG_Objects.PickupItems.AddRange(Find<GenericSmallPickupItem_Core>());

        LG_Objects.Ladders.AddRange(Find<LG_Ladder>());
        LG_Objects.Reactors.AddRange(Find<LG_WardenObjective_Reactor>());
        LG_Objects.GeneratorClusters.AddRange(Find<LG_PowerGeneratorCluster>());
        LG_Objects.Generators.AddRange(Find<LG_PowerGenerator_Core>());
    }

    static IEnumerable<T> Find<T>() where T : UnityEngine.Object
    {
        return LG_Objects.FindObjectsInLevel<T>();
    }
}

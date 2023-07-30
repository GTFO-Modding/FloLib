using GTFO.API;
using LevelGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Infos;
/// <summary>
/// Info Utility to List or Find Objects in Level
/// </summary>
public static partial class LG_Objects
{
    /// <summary>
    /// List Every <see cref="LG_SecurityDoor"/> In level
    /// </summary>
    public static ComponentList<LG_SecurityDoor> SecurityDoors { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="LG_WeakDoor"/> In level
    /// </summary>
    public static ComponentList<LG_WeakDoor> WeakDoors { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="LG_Light"/> In level
    /// </summary>
    public static ComponentList<LG_Light> Lights { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="LG_ComputerTerminal"/> In level
    /// </summary>
    public static ComponentList<LG_ComputerTerminal> Terminals { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="LG_LabDisplay"/> In level
    /// </summary>
    public static ComponentList<LG_LabDisplay> LabDisplays { get; private set; } = new ();


    /// <summary>
    /// List Every <see cref="LG_DoorButton"/> In level
    /// </summary>
    public static ComponentList<LG_DoorButton> DoorButtons { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="LG_WeakLock"/> In level
    /// </summary>
    public static ComponentList<LG_WeakLock> WeakLocks { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="LG_HSUActivator_Core"/> In level
    /// </summary>
    public static ComponentList<LG_HSUActivator_Core> HSUActivators { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="CarryItemPickup_Core"/> In level
    /// </summary>
    public static ComponentList<CarryItemPickup_Core> CarryItems { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="GenericSmallPickupItem_Core"/> In level
    /// </summary>
    public static ComponentList<GenericSmallPickupItem_Core> PickupItems { get; private set; } = new();


    /// <summary>
    /// List Every <see cref="LG_Ladder"/> In level
    /// </summary>
    public static ComponentList<LG_Ladder> Ladders { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="LG_WardenObjective_Reactor"/> In level
    /// </summary>
    public static ComponentList<LG_WardenObjective_Reactor> Reactors { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="LG_PowerGeneratorCluster"/> In level
    /// </summary>
    public static ComponentList<LG_PowerGeneratorCluster> GeneratorClusters { get; private set; } = new();
    /// <summary>
    /// List Every <see cref="LG_PowerGenerator_Core"/> In level
    /// </summary>
    public static ComponentList<LG_PowerGenerator_Core> Generators { get; private set; } = new();

    [AutoInvoke(InvokeWhen.PluginLoaded)]
    internal static void Init()
    {
        LevelAPI.OnLevelCleanup += OnLevelCleanup;
    }

    private static void OnLevelCleanup()
    {
        SecurityDoors.Clear();
        WeakDoors.Clear();
        Lights.Clear();
        Terminals.Clear();
        LabDisplays.Clear();

        DoorButtons.Clear();
        WeakLocks.Clear();
        HSUActivators.Clear();
        CarryItems.Clear();
        PickupItems.Clear();

        Ladders.Clear();
        Reactors.Clear();
        GeneratorClusters.Clear();
        Generators.Clear();
    }
}

using GameData;
using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGBuilder = LevelGeneration.Builder;

namespace FloLib.Infos;
public static partial class LG_Objects
{
    /// <summary>
    /// Find Object In Level
    /// </summary>
    /// <typeparam name="O">Object Type</typeparam>
    /// <param name="includeInactive">Include Inactive Objects?</param>
    /// <returns>Object In Level, <see langword="null"/> If nothing found</returns>
    public static O FindObjectInLevel<O>(bool includeInactive = false) where O : UnityEngine.Object
    {
        return LGBuilder.CurrentFloor.GetComponentInChildren<O>(includeInactive);
    }

    /// <summary>
    /// Find Objects In Level
    /// </summary>
    /// <typeparam name="O">Object Type</typeparam>
    /// <param name="includeInactive">Include Inactive Objects?</param>
    /// <returns>Objects In Level, Empty <see cref="IEnumerable{O}"/> If nothing found</returns>
    public static IEnumerable<O> FindObjectsInLevel<O>(bool includeInactive = false) where O : UnityEngine.Object
    {
        return LGBuilder.CurrentFloor.GetComponentsInChildren<O>(includeInactive);
    }

    /// <summary>
    /// Look for <see cref="LG_SecurityDoor"/> In Level (Reality Dimension)
    /// </summary>
    /// <param name="layer">Layer Type</param>
    /// <param name="localindex">LocalIndex of Zone where <see cref="LG_SecurityDoor"/> heading to</param>
    /// <returns>Targetted <see cref="LG_SecurityDoor"/>, <see langword="null"/> If not exists</returns>
    public static LG_SecurityDoor FindSecurityDoor(LG_LayerType layer, eLocalZoneIndex localindex)
    {
        return FindSecurityDoor(eDimensionIndex.Reality, layer, localindex);
    }

    /// <summary>
    /// Look for <see cref="LG_SecurityDoor"/> In Level
    /// </summary>
    /// <param name="dim">Dimension Type</param>
    /// <param name="layer">Layer Type</param>
    /// <param name="localindex">LocalIndex of Zone where <see cref="LG_SecurityDoor"/> heading to</param>
    /// <returns>Targetted <see cref="LG_SecurityDoor"/>, <see langword="null"/> If not exists</returns>
    public static LG_SecurityDoor FindSecurityDoor(eDimensionIndex dim, LG_LayerType layer, eLocalZoneIndex localindex)
    {
        LGBuilder.CurrentFloor.TryGetZoneByLocalIndex(dim, layer, localindex, out var zone);
        if (zone == null)
            return null;

        if (zone.m_sourceGate == null)
            return null;

        if (zone.m_sourceGate.SpawnedDoor == null)
            return null;

        var secDoor = zone.m_sourceGate.SpawnedDoor.TryCast<LG_SecurityDoor>();
        return secDoor;
    }
}

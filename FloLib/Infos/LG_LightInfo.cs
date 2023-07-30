using FloLib.Infos.Comps;
using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Infos;
public static class LG_LightInfo
{
    public struct Data
    {
        public float PrefabIntensity;
        public LightMode SpawnedMode;
        public float SpawnedIntensity;
        public Color SpawnedColor;
    }

    public static bool TryGetLightData(LG_Light light, out Data data)
    {
        if (light == null)
        {
            data = default;
            return false;
        }
        
        if (!light.TryGetComp(out LightData dataHolder))
        {
            data = default;
            return false;
        }

        data = dataHolder.CreateData();
        return true;
    }

    public static void RevertToSpawnedState(LG_Light light)
    {
        if (light == null)
        {
            return;
        }

        if (light.TryGetComp(out LightData dataHolder))
        {
            light.ChangeColor(dataHolder.SpawnedColor);
            light.ChangeIntensity(dataHolder.SpawnedIntensity);
        }
    }
}

public enum LightMode
{
    Off,
    On,
    Broken_Flickering
}

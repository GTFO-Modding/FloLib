using FloLib.Infos.Comps;
using HarmonyLib;
using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Infos.Inject;
[HarmonyPatch(typeof(LG_BuildZoneLightsJob), nameof(LG_BuildZoneLightsJob.Build))]

internal static class Inject_LG_LightBuild
{
    [HarmonyPriority(Priority.Last)]
    static void Prefix(LG_BuildZoneLightsJob __instance, out List<LG_Light> __state)
    {
        __state = new List<LG_Light>();
        foreach (var node in __instance.m_zone.m_courseNodes)
        {
            foreach (var light in node.m_area.GetComponentsInChildren<LG_Light>())
            {
                var lightData = light.gameObject.AddComponent<LightData>();
                if (light.TryCastToType<LG_SpotLight>(out var spotlight))
                {
                    lightData.PrefabIntensity.Set(spotlight.m_spotLight.intensity);
                }
                else if (light.TryCastToType<LG_PointLight>(out var pointlight))
                {
                    lightData.PrefabIntensity.Set(pointlight.m_pointLight.intensity);
                }
                else if (light.TryCastToType<LG_SpotLightAmbient>(out var spotAmbient))
                {
                    lightData.PrefabIntensity.Set(spotAmbient.m_spotLight.intensity);
                }
                else
                {
                    //Logger.Error($"Light does not match to any of light type! '{light.GetGameObjectPath()}'");
                    continue;
                }
                
                __state.Add(light);
            }
        }

        LG_Objects.Lights.AddRange(__state);
    }

    [HarmonyPriority(Priority.Last)]
    static void Postfix(List<LG_Light> __state)
    {
        foreach (var light in __state)
        {
            var lightData = light.GetComponent<LightData>();
            if (lightData == null)
            {
                Logger.Error($"Found Light without {nameof(LightData)} Component! {light.GetGameObjectPath()}");
                continue;
            }
            lightData.SpawnedIntensity = light.m_intensity;
            lightData.SpawnedColor = light.m_color;
            lightData.SpawnedMode = GetLightMode(light);
        }

        __state.Clear();
    }

    static LightMode GetLightMode(LG_Light light)
    {
        var cLight = light.GetC_Light();
        if (cLight.LightUpdator != null)
        {
            return LightMode.Broken_Flickering;
        }

        if (light.gameObject.active)
        {
            return LightMode.On;
        }
        else
        {
            return LightMode.Off;
        }
    }
}

using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils.Extensions;
public static class LGLightExtension
{
    public static void SetColor(this IEnumerable<LG_Light> lights, Color color)
    {
        foreach (var light in lights)
        {
            light.ChangeColor(color);
        }
    }

    public static void SetEnabled(this IEnumerable<LG_Light> lights, bool enabled)
    {
        foreach (var light in lights)
        {
            light.SetEnabled(enabled);
        }
    }

    public static bool Is(this LG_Light light, LightCategory lightCategory)
    {
        return light.m_category == (LG_Light.LightCategory)lightCategory;
    }

    public static bool IsAny(this LG_Light light, params LightCategory[] categories)
    {
        for (int i = 0; i<categories.Length; i++)
        {
            if (light.m_category == (LG_Light.LightCategory)categories[i])
                return true;
        }

        return false;
    }
}

public enum LightCategory
{
    General = LG_Light.LightCategory.General,
    Special = LG_Light.LightCategory.Special,
    Independent = LG_Light.LightCategory.Independent,
    Sign = LG_Light.LightCategory.Sign,
    Door = LG_Light.LightCategory.Door,
    DoorImportant = LG_Light.LightCategory.DoorImportant,
    Emergency = LG_Light.LightCategory.Emergency
}

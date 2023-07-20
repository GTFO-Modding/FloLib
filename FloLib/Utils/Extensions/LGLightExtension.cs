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
    /// <summary>
    /// Color change for list of LG_Light
    /// </summary>
    /// <param name="lights">LG_Light instances</param>
    /// <param name="color">New Color to use</param>
    public static void SetColor(this IEnumerable<LG_Light> lights, Color color)
    {
        foreach (var light in lights)
        {
            light.ChangeColor(color);
        }
    }

    /// <summary>
    /// Color Enabled State for list of LG_Light
    /// </summary>
    /// <param name="lights">LG_Light instances</param>
    /// <param name="enabled">New Enabled state to use</param>
    public static void SetEnabled(this IEnumerable<LG_Light> lights, bool enabled)
    {
        foreach (var light in lights)
        {
            light.SetEnabled(enabled);
        }
    }

    /// <summary>
    /// Check if Light Category type is matching
    /// </summary>
    /// <param name="light">Light To Compare with</param>
    /// <param name="lightCategory">LightCategory</param>
    /// <returns></returns>
    public static bool Is(this LG_Light light, LightCategory lightCategory)
    {
        return light.m_category == (LG_Light.LightCategory)lightCategory;
    }

    /// <summary>
    /// Check if Light Category is fall to any of given categories
    /// </summary>
    /// <param name="light">Light to Compare with</param>
    /// <param name="categories">List of allowed Categories</param>
    /// <returns></returns>
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

/// <summary>
/// Simplified LightCategory Type
/// </summary>
public enum LightCategory
{
    /// <summary>
    /// General Category
    /// </summary>
    General = LG_Light.LightCategory.General,

    /// <summary>
    /// Special Category
    /// </summary>
    Special = LG_Light.LightCategory.Special,

    /// <summary>
    /// Independent Category (Often used for Reactor Light)
    /// </summary>
    Independent = LG_Light.LightCategory.Independent,

    /// <summary>
    /// Sign Category
    /// </summary>
    Sign = LG_Light.LightCategory.Sign,

    /// <summary>
    /// Door Category
    /// </summary>
    Door = LG_Light.LightCategory.Door,

    /// <summary>
    /// Important Door Category (Often used for Security Doors)
    /// </summary>
    DoorImportant = LG_Light.LightCategory.DoorImportant,

    /// <summary>
    /// Emergency Light Category
    /// </summary>
    Emergency = LG_Light.LightCategory.Emergency
}

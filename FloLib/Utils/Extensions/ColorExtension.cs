using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils.Extensions;
/// <summary>
/// Extension Methods related to <see cref="Color"/>
/// </summary>
public static class ColorExtension
{
    /// <summary>
    /// Convert <see cref="Color"/> into RGB format Hex string (#RRGGBB)
    /// </summary>
    /// <param name="input"><see cref="Color"/> to convert</param>
    /// <returns>Hex string (#RRGGBB)</returns>
    public static string ToHex(this Color input)
    {
        return $"#{ColorUtility.ToHtmlStringRGB(input)}";
    }

    /// <summary>
    /// Convert <see cref="Color"/> into RGBA format Hex String (#RRGGBBAA)
    /// </summary>
    /// <param name="input"><see cref="Color"/> to convert</param>
    /// <returns>Hex string (#RRGGBBAA)</returns>
    public static string ToHexRGBA(this Color input)
    {
        return $"#{ColorUtility.ToHtmlStringRGBA(input)}";
    }

    /// <summary>
    /// Get Two <see cref="Color"/> information from given color; Base Color and Multiplier
    /// </summary>
    /// <param name="input">Input Color</param>
    /// <param name="baseColor">Output for Base Color</param>
    /// <param name="colorMultiplier">Output for Multiplier</param>
    public static void GetColorInfo(this Color input, out Color baseColor, out float colorMultiplier)
    {
        float highest_raw = 0.0f;
        float highest_abs = 0.0f;
        float r = input.r;
        float g = input.g;
        float b = input.b;
        float absR = Mathf.Abs(r);
        float absG = Mathf.Abs(g);
        float absB = Mathf.Abs(b);
        if (absR > highest_abs)
        {
            highest_abs = absR;
            highest_raw = r;
        }

        if (absG > highest_abs)
        {
            highest_abs = absG;
            highest_raw = g;
        }

        if (absB > highest_abs)
        {
            highest_abs = absB;
            highest_raw = b;
        }

        if (highest_abs > 0.0f)
        {
            baseColor = new Color(r / highest_raw, g / highest_raw, b / highest_raw, 1.0f);
            colorMultiplier = highest_raw;
        }
        else
        {
            baseColor = Color.black;
            colorMultiplier = 1.0f;
        }
    }
}

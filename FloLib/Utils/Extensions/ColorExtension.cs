using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils.Extensions;
public static class ColorExtension
{
    public static string ToHex(this Color input)
    {
        return $"#{ColorUtility.ToHtmlStringRGB(input)}";
    }

    public static string ToHexRGBA(this Color input)
    {
        return $"#{ColorUtility.ToHtmlStringRGBA(input)}";
    }

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

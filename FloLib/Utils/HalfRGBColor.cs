using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils;

/// <summary>
/// Half Precision Float RGB Color
/// </summary>
public struct HalfRGBColor
{
    /// <summary>
    /// Red Color Value;
    /// </summary>
    public Half R;

    /// <summary>
    /// Green Color Value;
    /// </summary>
    public Half G;

    /// <summary>
    /// Blue Color Value;
    /// </summary>
    public Half B;

    /// <summary>
    /// Implicit operator to convert HalfRGBColor to Color
    /// </summary>
    /// <param name="halfCol">HalfRGBColor to Convert</param>
    public static implicit operator Color(HalfRGBColor halfCol)
    {
        return new Color((float)halfCol.R, (float)halfCol.G, (float)halfCol.B);
    }

    /// <summary>
    /// Implicit operator to convert Color to HalfRGBColor
    /// </summary>
    /// <param name="col">Color to Convert</param>
    public static implicit operator HalfRGBColor(Color col)
    {
        var halfCol = new HalfRGBColor
        {
            R = (Half)col.r,
            G = (Half)col.g,
            B = (Half)col.b
        };
        return halfCol;
    }
}

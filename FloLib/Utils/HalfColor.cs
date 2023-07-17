using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils;

/// <summary>
/// Half Precision Float RGBA Color
/// </summary>
public struct HalfColor
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
    /// Alpha Color Value;
    /// </summary>
    public Half A;

    /// <summary>
    /// Ctor
    /// </summary>
    public HalfColor()
    {
        R = (Half)0.0f;
        G = (Half)0.0f;
        B = (Half)0.0f;
        A = (Half)0.0f;
    }

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="r">Red Color</param>
    /// <param name="g">Green Color</param>
    /// <param name="b">Blue Color</param>
    /// <param name="a">Alpha</param>
    public HalfColor(float r, float g, float b, float a)
    {
        R = (Half)r;
        G = (Half)g;
        B = (Half)b;
        A = (Half)a;
    }

    /// <summary>
    /// Implicit operator to convert HalfColor to Color
    /// </summary>
    /// <param name="halfCol">HalfColor to Convert</param>
    public static implicit operator Color(HalfColor halfCol)
    {
        return new Color((float)halfCol.R, (float)halfCol.G, (float)halfCol.B, (float)halfCol.A);
    }

    /// <summary>
    /// Implicit operator to convert Color to HalfColor
    /// </summary>
    /// <param name="col">Color to Convert</param>
    public static implicit operator HalfColor(Color col)
    {
        var halfCol = new HalfColor
        {
            R = (Half)col.r,
            G = (Half)col.g,
            B = (Half)col.b,
            A = (Half)col.a
        };
        return halfCol;
    }
}

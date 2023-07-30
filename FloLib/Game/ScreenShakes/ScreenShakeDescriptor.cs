using FloLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Game.ScreenShakes;
/// <summary>
/// 
/// </summary>
public struct ScreenShakeDescriptor
{
    public ScreenShakeMode Mode;
    public ScreenShakeModifier Modifier;
    public Vector3 Position;
    public float Intensity;
    public float Duration;
    public float FalloffStart;
    public float FalloffEnd;
    public EaseFunc.Type IntensityEasing;
}

public enum ScreenShakeMode : byte
{
    Global,
    PositionFalloff
}

public enum ScreenShakeModifier : byte
{
    PingPong,
    Increase,
    Decrease,
    Constant,
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Utils;
public struct BlinkInfo
{
    public float Duration;
    public float Speed;
    public bool EndBlinkState;

    public static readonly BlinkInfo InDefault = new()
    {
        Duration = 0.33f,
        Speed = 3.0f,
        EndBlinkState = true
    };

    public static readonly BlinkInfo OutDefault = new()
    {
        Duration = 0.33f,
        Speed = 3.0f,
        EndBlinkState = false
    };
}

public struct LerpInfo
{
    public float From;
    public float To;
    public float Duration;

    public LerpInfo(float from, float to, float duration)
    {
        From = from;
        To = to;
        Duration = duration;
    }
}
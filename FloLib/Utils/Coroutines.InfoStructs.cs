using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Utils;

/// <summary>
/// Information for <see cref="Coroutines.Blink(BlinkInfo, Action{bool})"/> or <see cref="Coroutines.DoBlink(BlinkInfo, Action{bool}, CoroutineLifeTime)"/>
/// </summary>
public struct BlinkInfo
{
    /// <summary>
    /// State of Blink when Coroutine has finished
    /// </summary>
    public bool EndBlinkState;

    /// <summary>
    /// Speed of Blink Coroutine
    /// </summary>
    public float Speed;

    /// <summary>
    /// Duration of Blink Coroutine
    /// </summary>
    public float Duration;

    /// <summary>
    /// Default Value for BlinkIn Coroutine (End is On)
    /// </summary>
    public static readonly BlinkInfo InDefault = new()
    {
        Duration = 0.33f,
        Speed = 3.0f,
        EndBlinkState = true
    };

    /// <summary>
    /// Default Value for BlinkOut Coroutine (End is Off)
    /// </summary>
    public static readonly BlinkInfo OutDefault = new()
    {
        Duration = 0.33f,
        Speed = 3.0f,
        EndBlinkState = false
    };
}

/// <summary>
/// Information for <see cref="Coroutines.Lerp(LerpInfo, Action{float})"/> or <see cref="Coroutines.DoLerp(LerpInfo, Action{float}, CoroutineLifeTime)"/>
/// </summary>
public struct LerpInfo
{
    /// <summary>
    /// Start Value
    /// </summary>
    public float From;

    /// <summary>
    /// End Value
    /// </summary>
    public float To;

    /// <summary>
    /// Duration of Lerping Coroutine
    /// </summary>
    public float Duration;

    /// <summary>
    /// Easing Type
    /// </summary>
    public EaseFunc.Type Easing;

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="from">Start Value</param>
    /// <param name="to">End Value</param>
    /// <param name="duration">Duration of Lerping Coroutine</param>
    /// <param name="ease">Ease Type</param>
    public LerpInfo(float from, float to, float duration, EaseFunc.Type ease = EaseFunc.Type.Linear)
    {
        From = from;
        To = to;
        Duration = duration;
        Easing = ease;
    }
}
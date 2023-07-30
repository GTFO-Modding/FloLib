using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils;

public static partial class Coroutines
{
    public static Coroutine DoWait(float delay, Action onDone, CoroutineLifeTime lifeTime = CoroutineLifeTime.Forever)
    {
        return Start(Wait(delay, onDone), lifeTime);
    }

    public static Coroutine DoWaitRealtime(float delay, Action onDone, CoroutineLifeTime lifeTime = CoroutineLifeTime.Forever)
    {
        return Start(WaitRealtime(delay, onDone), lifeTime);
    }

    public static Coroutine DoBlink(BlinkInfo blinkInfo, Action<bool> onBlinkChanged, CoroutineLifeTime lifeTime = CoroutineLifeTime.Forever)
    {
        return Start(Blink(blinkInfo, onBlinkChanged), lifeTime);
    }

    public static Coroutine DoLerp(LerpInfo lerpInfo, Action<float> onValueChanged, CoroutineLifeTime lifeTime = CoroutineLifeTime.Forever)
    {
        return Start(Lerp(lerpInfo, onValueChanged), lifeTime);
    }

    public static IEnumerator Wait(float delay, Action onDone)
    {
        yield return new WaitForSeconds(delay);
        onDone?.Invoke();
    }

    public static IEnumerator WaitRealtime(float delay, Action onDone)
    {
        yield return new WaitForSecondsRealtime(delay);
        onDone?.Invoke();
    }

    public static IEnumerator Blink(BlinkInfo info, Action<bool> onBlinkChanged)
    {
        var time = 0.0f;
        var lastCond = false;
        onBlinkChanged?.Invoke(false);
        while (time < info.Duration)
        {
            var cond = BlinkByProgress(Mathf.Repeat(time * info.Speed, 1.0f));
            if (cond != lastCond)
            {
                onBlinkChanged?.Invoke(cond);
                lastCond = cond;
            }
            time += Time.deltaTime;
            yield return null;
        }
        onBlinkChanged?.Invoke(info.EndBlinkState);
    }

    public static IEnumerator Lerp(LerpInfo info, Action<float> onValueChanged)
    {
        var time = 0.0f;
        onValueChanged?.Invoke(info.From);
        while (time < info.Duration)
        {
            var progress = info.Easing.Evaluate(time / info.Duration);
            var value = Mathf.Lerp(info.From, info.To, progress);
            onValueChanged?.Invoke(value);
            time += Time.deltaTime;
            yield return null;
        }
        onValueChanged?.Invoke(info.To);
    }

    private static bool BlinkByProgress(float progress)
    {
        return (progress % 0.25f) < 0.125f;
    }
}

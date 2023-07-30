using BepInEx.Unity.IL2CPP.Utils;
using FloLib.Infos;
using FloLib.Networks;
using FloLib.Utils;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Game.ScreenShakes;
public sealed class ScreenShake
{
    [AutoInvoke(InvokeWhen.StartupAssetLoaded)]
    internal static void Init()
    {
        GlobalNetAction<ScreenShakeDescriptor>.OnReceive += OnReceive;
    }

    public static void Trigger(ScreenShakeDescriptor data)
    {
        GlobalNetAction<ScreenShakeDescriptor>.Send(data);
    }

    private static void OnReceive(ulong sender, ScreenShakeDescriptor data)
    {
        if (!LocalPlayer.TryGetAgent(out var localPlayer))
            return;

        var camera = localPlayer.FPSCamera;
        camera.StartCoroutine(DoShake(camera, data));
    }

    private static IEnumerator DoShake(FPSCamera camera, ScreenShakeDescriptor data)
    {
        var time = 0.0f;
        while (time < data.Duration)
        {
            var distFactor = 1.0f;
            if (data.Mode == ScreenShakeMode.PositionFalloff)
            {
                var distance = Vector3.Distance(data.Position, camera.Position);
                distFactor = Mathf.InverseLerp(data.FalloffEnd, data.FalloffStart, distance);
            }

            var timeFactor = data.Modifier switch
            {
                ScreenShakeModifier.PingPong => Mathf.PingPong(Mathf.InverseLerp(data.Duration, 0.0f, time) * 2.0f, 1.0f),
                ScreenShakeModifier.Increase => Mathf.InverseLerp(0.0f, data.Duration, time),
                ScreenShakeModifier.Decrease => Mathf.InverseLerp(data.Duration, 0.0f, time),
                _ => 1.0f
            };

            timeFactor = data.IntensityEasing.Evaluate(timeFactor);

            var amount = distFactor * timeFactor * data.Intensity;
            camera.SetConstantCameraShakeAmount(amount);
            time += Time.deltaTime;
            yield return null;
        }
        camera.SetConstantCameraShakeAmount(0.0f);
    }
}

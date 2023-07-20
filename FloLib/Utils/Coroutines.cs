using BepInEx.Unity.IL2CPP.Utils;
using FloLib.Networks.Inject;
using FloLib.Utils.Handlers;
using GTFO.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils;

/// <summary>
/// Type of Coroutine Life-time
/// </summary>
public enum CoroutineLifeTime
{
    /// <summary>
    /// Never stop the coroutine unless coroutine itself finished
    /// </summary>
    Forever,

    /// <summary>
    /// Stop the coroutine when lobby has changed
    /// </summary>
    Lobby,

    /// <summary>
    /// Stop the coroutine when level has cleaned up
    /// </summary>
    Level,

    /// <summary>
    /// Stop the coroutine when recall buffer has loaded (Checkpoint loading, Host migration) or level has cleaned up
    /// </summary>
    BetweenRecall
}

/// <summary>
/// Utility for Coroutine Management and pre-defined Coroutines
/// </summary>
public static partial class Coroutines
{
    private static MonoBehaviour _Runner;
    private static readonly Queue<Coroutine> _Coroutines_Lobby = new();
    private static readonly Queue<Coroutine> _Coroutines_Level = new();
    private static readonly Queue<Coroutine> _Coroutines_CPLoad = new();

    [AutoInvoke(InvokeWhen.StartGame)]
    internal static void Init()
    {
        var obj = new GameObject();
        UnityEngine.Object.DontDestroyOnLoad(obj);
        _Runner = obj.AddComponent<EmptyMB>();

        LevelAPI.OnLevelCleanup += LevelAPI_OnLevelCleanup;
        Inject_SNet_Capture.OnBufferRecalled += Inject_SNet_Capture_OnBufferRecalled;
    }

    private static void Inject_SNet_Capture_OnBufferRecalled(SNetwork.eBufferType obj)
    {
        StopAll(CoroutineLifeTime.BetweenRecall);
    }

    private static void LevelAPI_OnLevelCleanup()
    {
        StopAll(CoroutineLifeTime.Level);
        StopAll(CoroutineLifeTime.BetweenRecall);
    }

    /// <summary>
    /// Stop All Coroutines
    /// </summary>
    /// <param name="lifeTime">Desired Category to stop</param>
    /// <exception cref="NotSupportedException"></exception>
    public static void StopAll(CoroutineLifeTime lifeTime = CoroutineLifeTime.Level)
    {
        Queue<Coroutine> queueToClear = lifeTime switch
        {
            CoroutineLifeTime.Lobby => _Coroutines_Lobby,
            CoroutineLifeTime.Level => _Coroutines_Level,
            CoroutineLifeTime.BetweenRecall => _Coroutines_CPLoad,
            _ => throw new NotSupportedException($"{nameof(CoroutineLifeTime)}: {lifeTime} is not supported!"),
        };

        while (queueToClear.TryDequeue(out var coroutine))
        {
            _Runner.StopCoroutine(coroutine);
        }
    }

    /// <summary>
    /// Stop Coroutine by <see cref="Coroutine"/> instance
    /// </summary>
    /// <param name="coroutine"></param>
    public static void Stop(Coroutine coroutine)
    {
        _Runner.StopCoroutine(coroutine);
    }

    /// <summary>
    /// Start Coroutine with given <see cref="CoroutineLifeTime"/>
    /// </summary>
    /// <param name="coroutine">Coroutine to Start</param>
    /// <param name="lifeTime">Life Time for Coroutine</param>
    /// <returns><see cref="Coroutine"/> instance to use on <see cref="Stop(Coroutine)"/></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Coroutine Start(IEnumerator coroutine, CoroutineLifeTime lifeTime = CoroutineLifeTime.Forever)
    {
        Coroutine coroutineRef;
        switch (lifeTime)
        {
            case CoroutineLifeTime.Forever:
                coroutineRef = _Runner.StartCoroutine(coroutine);
                return coroutineRef;

            case CoroutineLifeTime.Lobby:
                coroutineRef = _Runner.StartCoroutine(coroutine);
                _Coroutines_Lobby.Enqueue(coroutineRef);
                return coroutineRef;

            case CoroutineLifeTime.Level:
                coroutineRef = _Runner.StartCoroutine(coroutine);
                _Coroutines_Level.Enqueue(coroutineRef);
                return coroutineRef;

            case CoroutineLifeTime.BetweenRecall:
                coroutineRef = _Runner.StartCoroutine(coroutine);
                _Coroutines_CPLoad.Enqueue(coroutineRef);
                return coroutineRef;

            default:
                throw new NotSupportedException($"{nameof(CoroutineLifeTime)}: {lifeTime} is not supported!!");
        }
    }
}

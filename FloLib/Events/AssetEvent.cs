using AssetShards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Events;
/// <summary>
/// Events Related to <see cref="AssetShardManager"/>
/// </summary>
public static class AssetEvent
{
    /// <summary>
    /// Invoked When <see cref="AssetShardManager.OnStartupAssetsLoaded"/> has invoked
    /// </summary>
    public static event Action OnStartupAssetLoaded;
    /// <summary>
    /// Invoked When <see cref="AssetShardManager.OnEnemyAssetsLoaded"/> has invoked
    /// </summary>
    public static event Action OnEnemyAssetLoaded;
    /// <summary>
    /// Invoked When <see cref="AssetShardManager.OnSharedAsssetLoaded"/> has invoked
    /// </summary>
    public static event Action OnSharedAssetLoaded;
    /// <summary>
    /// Invoked When all [<see cref="AssetShardManager.OnStartupAssetsLoaded"/>], [<see cref="AssetShardManager.OnEnemyAssetsLoaded"/>] and [<see cref="AssetShardManager.OnSharedAsssetLoaded"/>] has invoked
    /// </summary>
    public static event Action OnAllAssetsLoaded;

    private static bool _IsStartupLoaded = false;
    private static bool _IsEnemyLoaded = false;
    private static bool _IsSharedLoaded = false;
    private static bool _IsAllLoaded = false;

    internal static void Invoke_StartupAssetLoaded()
    {
        if (_IsStartupLoaded)
            return;

        _IsStartupLoaded = true;
        OnStartupAssetLoaded?.Invoke();
        CheckAllAssetsLoaded();
    }

    internal static void Invoke_EnemyAssetLoaded()
    {
        if (_IsEnemyLoaded)
            return;

        _IsEnemyLoaded = true;
        OnEnemyAssetLoaded?.Invoke();
        CheckAllAssetsLoaded();
    }

    internal static void Invoke_SharedAssetLoaded()
    {
        if (_IsSharedLoaded)
            return;

        _IsSharedLoaded = true;
        OnSharedAssetLoaded?.Invoke();
        CheckAllAssetsLoaded();
    }

    private static void CheckAllAssetsLoaded()
    {
        if (_IsAllLoaded)
            return;

        _IsAllLoaded = true;
        OnAllAssetsLoaded?.Invoke();
    }
}

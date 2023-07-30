using AssetShards;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Attributes;
/// <summary>
/// Attribute to specify static methods or constructor to be automatically invoke when specific event see also: <see cref="Automation.RegisterTypes(Type)"/>
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
public sealed class AutoInvokeAttribute : Attribute
{
    internal object[] Arguments = Array.Empty<object>();
    internal InvokeWhen When = InvokeWhen.PluginLoaded;

    /// <summary>
    /// Default Constructor
    /// </summary>
    public AutoInvokeAttribute(InvokeWhen when)
    {
        When = when;
    }

    /// <summary>
    /// Constrctor when arguments needs to be passed
    /// </summary>
    public AutoInvokeAttribute(InvokeWhen when, params object[] arguments)
    {
        When = when;
        Arguments = arguments;
    }
}

/// <summary>
/// Invoke Timing for <see cref="AutoInvokeAttribute"/>
/// </summary>
public enum InvokeWhen
{
    /// <summary>
    /// When Plugin Loaded (Right after <see cref="Automation.RegisterTypes()"/> called)
    /// </summary>
    PluginLoaded,

    /// <summary>
    /// When <see cref="StartMainGame.Awake"/> has invoked (when the very first scene has loaded)
    /// </summary>
    StartGame,

    /// <summary>
    /// When <see cref="AssetShardManager.OnStartupAssetsLoaded"/> has invoked
    /// </summary>
    StartupAssetLoaded,

    /// <summary>
    /// When <see cref="AssetShardManager.OnEnemyAssetsLoaded"/> has invoked
    /// </summary>
    EnemyAssetLoaded,

    /// <summary>
    /// When <see cref="AssetShardManager.OnSharedAsssetLoaded"/> has invoked
    /// </summary>
    SharedAssetLoaded,

    /// <summary>
    /// When all [<see cref="AssetShardManager.OnStartupAssetsLoaded"/>], [<see cref="AssetShardManager.OnEnemyAssetsLoaded"/>] and [<see cref="AssetShardManager.OnSharedAsssetLoaded"/>] has invoked
    /// </summary>
    AllAssetsLoaded
}

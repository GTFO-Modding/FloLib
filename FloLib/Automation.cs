using AssetShards;
using FloLib.Attributes;
using FloLib.Events;
using GTFO.API;
using Il2CppInterop.Runtime.Injection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FloLib;
/// <summary>
/// Provide Automatic Invoke and ClassInjection
/// </summary>
public static class Automation
{
    private const BindingFlags ALL = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    private readonly static Queue<(MethodInfo method, object[] args)> _InvokeWhenStartGame = new();
    private readonly static Queue<(MethodInfo method, object[] args)> _InvokeWhenStartupAssetLoaded = new();
    private readonly static Queue<(MethodInfo method, object[] args)> _InvokeWhenEnemyAssetLoaded = new();
    private readonly static Queue<(MethodInfo method, object[] args)> _InvokeWhenSharedAssetLoaded = new();
    private readonly static Queue<(MethodInfo method, object[] args)> _InvokeWhenAllAssetsLoaded = new();

    static Automation()
    {
        StartGameEvent.OnGameLoaded += () =>
        {
            while (_InvokeWhenStartGame.TryDequeue(out var item))
            {
                RunMethod(item.method, item.args);
            }
        };

        AssetEvent.OnStartupAssetLoaded += () =>
        {
            while (_InvokeWhenStartupAssetLoaded.TryDequeue(out var item))
            {
                RunMethod(item.method, item.args);
            }
        };

        AssetEvent.OnEnemyAssetLoaded += () =>
        {
            while (_InvokeWhenEnemyAssetLoaded.TryDequeue(out var item))
            {
                RunMethod(item.method, item.args);
            }
        };

        AssetEvent.OnSharedAssetLoaded += () =>
        {
            while (_InvokeWhenSharedAssetLoaded.TryDequeue(out var item))
            {
                RunMethod(item.method, item.args);
            }
        };

        AssetEvent.OnAllAssetsLoaded += () =>
        {
            while (_InvokeWhenAllAssetsLoaded.TryDequeue(out var item))
            {
                RunMethod(item.method, item.args);
            }
        };
    }

    /// <summary>
    /// Get Types to Register from Caller Assembly
    /// </summary>
    /// <exception cref="NullReferenceException">Caller Assembly is unknown</exception>
    public static void RegisterTypes()
    {
        var targetAssem = new StackFrame(1).GetMethod()?.GetType()?.Assembly ?? null;
        if (targetAssem == null)
            throw new NullReferenceException("Caller Assembly was null");

        RegisterTypes(targetAssem);
    }

    /// <summary>
    /// Get Types to Register from Target type's assembly
    /// </summary>
    /// <param name="target">Type from target assembly</param>
    /// <exception cref="ArgumentNullException">target type is null</exception>
    public static void RegisterTypes(Type target)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        var assem = target.Assembly;
        RegisterTypes(assem);
    }

    /// <summary>
    /// Get Types to Register from Target assembly
    /// </summary>
    /// <param name="target">Target assembly</param>
    /// <exception cref="ArgumentNullException">Target Assembly is null</exception>
    public static void RegisterTypes(Assembly target)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        InjectAll(target);
        AddAutoInvokes(target);
    }

    private static void InjectAll(Assembly assem)
    {
        var types = assem
            .GetTypes()?
            .Where(x=>Attribute.IsDefined(x, typeof(AutoInjectAttribute))) ?? Enumerable.Empty<Type>();

        foreach (var type in types)
        {
            var attribute = (AutoInjectAttribute)Attribute.GetCustomAttribute(type, typeof(AutoInjectAttribute));
            if (attribute.Interfaces.Length > 0)
            {
                ClassInjector.RegisterTypeInIl2Cpp(type, new RegisterTypeOptions()
                {
                    Interfaces = attribute.Interfaces,
                    LogSuccess = RegisterTypeOptions.Default.LogSuccess
                });
            }
            else
            {
                ClassInjector.RegisterTypeInIl2Cpp(type);
            }
        }
    }

    private static void AddAutoInvokes(Assembly assem)
    {
        var methods = assem
            .GetTypes()?
            .SelectMany(x => x.GetMethods(ALL))?
            .Where(x => x.IsStatic && Attribute.IsDefined(x, typeof(AutoInvokeAttribute))) ?? Enumerable.Empty<MethodInfo>();

        foreach (var method in methods)
        {
            var attribute = (AutoInvokeAttribute)Attribute.GetCustomAttribute(method, typeof(AutoInvokeAttribute));
            var args = attribute.Arguments;
            var when = attribute.When;

            switch (when)
            {
                case InvokeWhen.PluginLoaded:
                    RunMethod(method, args);
                    break;

                case InvokeWhen.StartGame:
                    _InvokeWhenStartGame.Enqueue((method, args));
                    break;

                case InvokeWhen.StartupAssetLoaded:
                    _InvokeWhenStartupAssetLoaded.Enqueue((method, args));
                    break;

                case InvokeWhen.EnemyAssetLoaded:
                    _InvokeWhenEnemyAssetLoaded.Enqueue((method, args));
                    break;

                case InvokeWhen.SharedAssetLoaded:
                    _InvokeWhenSharedAssetLoaded.Enqueue((method, args));
                    break;

                case InvokeWhen.AllAssetsLoaded:
                    _InvokeWhenAllAssetsLoaded.Enqueue((method, args));
                    break;
            }
        }
    }

    private static void RunMethod(MethodInfo method, params object[] args)
    {
        if (method.IsConstructor)
        {
            RuntimeHelpers.RunClassConstructor(method.DeclaringType.TypeHandle);
        }
        else
        {
            method.Invoke(null, args);
        }
    }
}

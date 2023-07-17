using FloLib.Attributes;
using FloLib.Events;
using GTFO.API;
using Il2CppInterop.Runtime.Injection;
using System;
using System.Collections.Generic;
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

    static Automation()
    {
        StartGameEvent.OnGameLoaded += () =>
        {
            while (_InvokeWhenStartGame.TryDequeue(out var item))
            {
                RunMethod(item.method, item.args);
            }
        };

        AssetAPI.OnStartupAssetsLoaded += () =>
        {
            while(_InvokeWhenStartupAssetLoaded.TryDequeue(out var item))
            {
                RunMethod(item.method, item.args);
            }
        };
    }

    public static void RegisterTypes(Type target)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        var assem = target.Assembly;
        if (assem == null)
            throw new NullReferenceException("Target Type Assembly is null");

        InjectAll(assem);
        AddAutoInvokes(assem);
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

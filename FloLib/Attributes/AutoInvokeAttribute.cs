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

public enum InvokeWhen
{
    PluginLoaded,
    StartGame,
    StartupAssetLoaded,
}

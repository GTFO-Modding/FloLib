using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Attributes;
/// <summary>
/// Attribute to specify class to injected to il2cpp when plugin loaded see also: <see cref="Automation.RegisterTypes(Type)"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class AutoInjectAttribute : Attribute
{
    internal Type[] Interfaces = Array.Empty<Type>();

    /// <summary>
    /// Default Constructor
    /// </summary>
    public AutoInjectAttribute()
    {
        
    }

    /// <summary>
    /// Constrctor when il2cpp interfaces are need to specify
    /// </summary>
    public AutoInjectAttribute(params Type[] il2cppInterfaces)
    {
        Interfaces = il2cppInterfaces;
    }
}

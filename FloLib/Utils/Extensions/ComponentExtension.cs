using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils.Extensions;
/// <summary>
/// Extension Methods related to <see cref="Component"/>
/// </summary>
[SuppressMessage("Type Safety", "UNT0014:Invalid type for call to GetComponent", Justification = "<Pending>")]
public static class ComponentExtension
{
    /// <summary>
    /// IL2CPP-safe TryGetComponent, 
    /// </summary>
    /// <typeparam name="T">Component Type to find</typeparam>
    /// <param name="comp">Component to find from</param>
    /// <param name="component">Output result of component: null if none</param>
    /// <returns><see langword="true"/> if Component exists / <see langword="false"/> if doesn't exist</returns>

    public static bool TryGetComp<T>(this Component comp, out T component)
    {
        component = comp.GetComponent<T>();
        return component != null;
    }

    /// <summary>
    /// Get <see cref="GUIX_VirtualScene"/> using <see cref="GUIX_VirtualSceneLink"/>
    /// </summary>
    /// <param name="comp">Component to find attached <see cref="GUIX_VirtualScene"/></param>
    /// <param name="scene">Output result</param>
    /// <returns><see langword="true"/> if <see cref="GUIX_VirtualScene"/> exists / <see langword="false"/> if doesn't exist</returns>
    public static bool TryGetVirtualScene(this Component comp, out GUIX_VirtualScene scene)
    {
        var link = comp.GetComponent<GUIX_VirtualSceneLink>();
        if (link == null)
        {
            scene = null;
            return false;
        }

        scene = link.m_virtualScene;
        return scene != null;
    }

    /// <summary>
    /// Get Path String for underlying <see cref="GameObject"/>
    /// </summary>
    /// <param name="comp">Base Component to Get a Path</param>
    /// <returns>Full Path to the GameObject</returns>
    public static string GetGameObjectPath(this Component comp)
    {
        return comp.gameObject.GetPath();
    }
}

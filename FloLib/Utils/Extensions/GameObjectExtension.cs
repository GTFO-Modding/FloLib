using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils.Extensions;
/// <summary>
/// Extension Methods related to <see cref="GameObject"/>
/// </summary>
[SuppressMessage("Type Safety", "UNT0014:Invalid type for call to GetComponent", Justification = "<Pending>")]
public static class GameObjectExtension
{
    /// <summary>
    /// IL2CPP-safe TryGetComponent, 
    /// </summary>
    /// <typeparam name="T">Component Type to find</typeparam>
    /// <param name="obj">GameObject to find</param>
    /// <param name="component">Output result of component: null if none</param>
    /// <returns><see langword="true"/> if Component exists / <see langword="false"/>false if doesn't exist</returns>

    public static bool TryGetComp<T>(this GameObject obj, out T component)
    {
        component = obj.GetComponent<T>();
        return component != null;
    }

    /// <summary>
    /// Look for Component In Parent or Child, Useful for looking LG Components with colliders
    /// </summary>
    /// <typeparam name="T">Component Type to find</typeparam>
    /// <param name="obj">GameObject to find</param>
    /// <returns><see langword="true"/> if Component exists / <see langword="false"/>false if doesn't exist</returns>
    public static T GetCompInParentOrChild<T>(this GameObject obj)
    {
        T comp = obj.GetComponentInParent<T>();
        if (comp == null)
        {
            comp = obj.GetComponentInChildren<T>();
            if (comp == null)
            {
                obj.GetComponent<T>();
            }
        }
        return comp;
    }
}

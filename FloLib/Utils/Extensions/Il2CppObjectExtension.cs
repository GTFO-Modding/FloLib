using Il2CppInterop.Runtime.InteropTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Utils.Extensions;
/// <summary>
/// Extension Methods related to <see cref="Il2CppObjectBase"/>
/// </summary>
public static class Il2CppObjectExtension
{
    /// <summary>
    /// Check If given object can be casted into Type
    /// </summary>
    /// <typeparam name="T">Type to cast</typeparam>
    /// <param name="obj">Object to cast</param>
    /// <returns><see langword="true"/> if It can be casted</returns>
    public static bool CanCastToType<T>(this Il2CppObjectBase obj) where T : Il2CppObjectBase
    {
        return obj.TryCast<T>() != null;
    }

    /// <summary>
    /// Try Cast given object to Type
    /// </summary>
    /// <typeparam name="T">Type to cast</typeparam>
    /// <param name="obj">Object to cast</param>
    /// <param name="result">Casted Object</param>
    /// <returns><see langword="true"/> if It can be casted</returns>
    public static bool TryCastToType<T>(this Il2CppObjectBase obj, out T result) where T : Il2CppObjectBase
    {
        result = obj.TryCast<T>();
        return result != null;
    }
}

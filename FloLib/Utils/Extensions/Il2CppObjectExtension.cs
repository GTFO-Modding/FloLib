using Il2CppInterop.Runtime.InteropTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Utils.Extensions;
public static class Il2CppObjectExtension
{
    public static bool CanCastToType<T>(this Il2CppObjectBase obj) where T : Il2CppObjectBase
    {
        return obj.TryCast<T>() != null;
    }

    public static bool TryCastToType<T>(this Il2CppObjectBase obj, out T result) where T : Il2CppObjectBase
    {
        result = obj.TryCast<T>();
        return result != null;
    }
}

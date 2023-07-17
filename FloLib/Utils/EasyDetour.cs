using BepInEx.Unity.IL2CPP.Hook;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Utils;

/// <summary>
/// Utility Class to Create Detour in ease
/// </summary>
public unsafe static class EasyDetour
{
    /// <summary>
    /// Delegate for static void method Detour
    /// </summary>
    /// <param name="methodInfo">Pointer of methodInfo (Use <see cref="UnityVersionHandler.Wrap(Il2CppMethodInfo*)"/> to access)</param>
    public delegate void StaticVoidDelegate(Il2CppMethodInfo* methodInfo);

    /// <summary>
    /// Delegate for instance void method Detour
    /// </summary>
    /// <param name="instancePtr">Pointer of instance (Use type's ctor to access on managed side)</param>
    /// <param name="methodInfo">Pointer of methodInfo (Use <see cref="UnityVersionHandler.Wrap(Il2CppMethodInfo*)"/> to access)</param>
    public delegate void InstanceVoidDelegate(IntPtr instancePtr, Il2CppMethodInfo* methodInfo);

    /// <summary>
    /// Try Creating NativeDetour with given descriptor
    /// </summary>
    /// <typeparam name="T">Delegate Type - NOTE: Type should be <see cref="IntPtr"/> and <see cref="bool"/> should be <see cref="byte"/></typeparam>
    /// <param name="descriptor">Detour Description</param>
    /// <param name="to">Detour Delegate; This will be invoked when method has called</param>
    /// <param name="originalCall">Original Method Delegate; Invoke this to call original method</param>
    /// <param name="detourInstance">NativeDetour instance for control; This MUST be saved on managed domain in order to avoid GC crash!</param>
    /// <returns></returns>
    public static bool TryCreate<T>(DetourDescriptor descriptor, T to, out T originalCall, out INativeDetour detourInstance) where T : Delegate
    {
        try
        {
            var ptr = descriptor.GetMethodPointer();
            detourInstance = INativeDetour.CreateAndApply(ptr, to, out originalCall);
            return detourInstance != null;
        }
        catch(Exception e)
        {
            Logger.Error($"Exception Thrown while creating Detour:");
            Logger.Error(e.ToString());
        }

        originalCall = null;
        detourInstance = null;
        return false;
    }
}

/// <summary>
/// Descriptor for Finding the Il2Cpp Method Pointer
/// </summary>
public struct DetourDescriptor
{
    /// <summary>
    /// IL2CPP Type to look for method
    /// </summary>
    public Type Type;

    /// <summary>
    /// Method's Return Type (You must use typeof(void) if it doesn't have return)
    /// </summary>
    public Type ReturnType;

    /// <summary>
    /// Array of Argument Types
    /// </summary>
    public Type[] ArgTypes;

    /// <summary>
    /// Name of Method
    /// </summary>
    public string MethodName;

    /// <summary>
    /// Is Method or Type is generic?
    /// </summary>
    public bool IsGeneric;

    /// <summary>
    /// Look for Il2Cpp Method Pointer
    /// </summary>
    /// <returns>Pointer of Il2Cpp Method</returns>
    /// <exception cref="MissingFieldException"></exception>
    public unsafe nint GetMethodPointer()
    {
        if (Type == null)
        {
            throw new MissingFieldException($"Field {nameof(Type)} is not set!");
        }

        if (ReturnType == null)
        {
            throw new MissingFieldException($"Field {nameof(ReturnType)} is not set! If you mean 'void' do typeof(void)");
        }

        if (string.IsNullOrEmpty(MethodName))
        {
            throw new MissingFieldException($"Field {nameof(MethodName)} is not set or valid!");
        }

        var type = Il2CppType.From(Type, throwOnFailure: true);
        var typePtr = Il2CppClassPointerStore.GetNativeClassPointer(Type);

        var returnTypeName = GetFullName(ReturnType);
        string[] argTypeNames;
        if (ArgTypes == null || ArgTypes.Length <= 0)
        {
            argTypeNames = Array.Empty<string>();
        }
        else
        {
            var length = ArgTypes.Length;
            argTypeNames = new string[length];
            for (int i = 0; i < length; i++)
            {
                var argType = ArgTypes[i];
                argTypeNames[i] = GetFullName(argType);
            }
        }

        var methodPtr = (void**)IL2CPP.GetIl2CppMethod(typePtr, IsGeneric, MethodName, returnTypeName, argTypeNames).ToPointer();
        if (methodPtr == null)
        {
            return (nint)methodPtr;
        }

        return (nint)(*methodPtr);
    }

    private static string GetFullName(Type type)
    {
        bool isPointer = type.IsPointer;
        if (isPointer)
        {
            type = type.GetElementType();
        }

        if (type.IsPrimitive || type == typeof(string))
        {
            if (isPointer) return type.MakePointerType().FullName;
            else return type.FullName;
        }
        else
        {
            var il2cppType = Il2CppType.From(type, throwOnFailure: true);
            if (isPointer) return il2cppType.MakePointerType().FullName;
            else return il2cppType.FullName;
        }
    }
}
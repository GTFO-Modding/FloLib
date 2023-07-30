using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Utils.Extensions;
/// <summary>
/// Extension Methods to Box Primitive Type into <see cref="Il2CppSystem.Object"/>
/// </summary>
public static class Il2CppBoxingExtension
{
    /// <summary>
    /// Box <see langword="bool"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this bool value)
    {
        return new Il2CppSystem.Boolean
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="byte"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this byte value)
    {
        return new Il2CppSystem.Byte
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="sbyte"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this sbyte value)
    {
        return new Il2CppSystem.SByte
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="char"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this char value)
    {
        return new Il2CppSystem.Char
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="short"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this short value)
    {
        return new Il2CppSystem.Int16
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="ushort"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this ushort value)
    {
        return new Il2CppSystem.UInt16
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="int"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this int value)
    {
        return new Il2CppSystem.Int32
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="uint"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this uint value)
    {
        return new Il2CppSystem.UInt32
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="long"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this long value)
    {
        return new Il2CppSystem.Int64
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="ulong"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this ulong value)
    {
        return new Il2CppSystem.UInt64
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="float"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this float value)
    {
        return new Il2CppSystem.Single
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="double"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this double value)
    {
        return new Il2CppSystem.Double
        {
            m_value = value
        }.BoxIl2CppObject();
    }

    /// <summary>
    /// Box <see langword="nint"/> value into <see cref="Il2CppSystem.Object"/>
    /// </summary>
    /// <param name="value">Value to box</param>
    /// <returns>Boxed <see cref="Il2CppSystem.Object"/></returns>
    public static Il2CppSystem.Object BoxToIl2CppObject(this nint value)
    {
        return new Il2CppSystem.IntPtr
        {
            m_value = value
        }.BoxIl2CppObject();
    }
}

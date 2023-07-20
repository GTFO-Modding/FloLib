using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils.Extensions;
/// <summary>
/// Extension Methods related to <see cref="Vector2"/>, <see cref="Vector3"/> and <see cref="Vector4"/> 
/// </summary>
public static class VectorExtension
{
    /// <summary>
    /// Return Formatted Vector string (x: 0.0000, y: 0.0000)
    /// </summary>
    /// <param name="vector">Input Vector</param>
    /// <returns>Formatted Vector string (x: 0.0000, y: 0.0000)</returns>
    public static string ToFormattedString(this Vector2 vector)
    {
        return $"x: {vector.x:0.0000}, y: {vector.y:0.0000}";
    }

    /// <summary>
    /// Return Formatted Vector string (x: 0.0000, y: 0.0000, z: 0.0000)
    /// </summary>
    /// <param name="vector">Input Vector</param>
    /// <returns>Formatted Vector string (x: 0.0000, y: 0.0000, z: 0.0000)</returns>
    public static string ToFormattedString(this Vector3 vector)
    {
        return $"x: {vector.x:0.0000}, y: {vector.y:0.0000} z: {vector.z:0.0000}";
    }

    /// <summary>
    /// Return Formatted Vector string (x: 0.0000, y: 0.0000, z: 0.0000, w: 0.0000)
    /// </summary>
    /// <param name="vector">Input Vector</param>
    /// <returns>Formatted Vector string (x: 0.0000, y: 0.0000, z: 0.0000, w: 0.0000)</returns>
    public static string ToFormattedString(this Vector4 vector)
    {
        return $"x: {vector.x:0.0000}, y: {vector.y:0.0000} z: {vector.z:0.0000} w: {vector.w:0.0000}";
    }
}

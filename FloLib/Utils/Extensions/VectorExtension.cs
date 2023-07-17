using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils.Extensions;
public static class VectorExtension
{
    public static string ToFormattedString(this Vector2 vector)
    {
        return $"x: {vector.x:0.0000}, y: {vector.y:0.0000}";
    }

    public static string ToFormattedString(this Vector3 vector)
    {
        return $"x: {vector.x:0.0000}, y: {vector.y:0.0000} z: {vector.z:0.0000}";
    }

    public static string ToFormattedString(this Vector4 vector)
    {
        return $"x: {vector.x:0.0000}, y: {vector.y:0.0000} z: {vector.z:0.0000} w: {vector.w:0.0000}";
    }
}

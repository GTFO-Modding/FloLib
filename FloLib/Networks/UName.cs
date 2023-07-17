using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Networks;
internal static class UName
{
    private readonly static SHA1 SHA = SHA1.Create();

    public static string Get(Type type, string prefix)
    {
        return prefix + GetHash(type.FullName);
    }

    public static string GetHash(string text)
    {
        var bytes = SHA.ComputeHash(Encoding.Unicode.GetBytes(text + "this is salt text, Awesome!"));
        return Convert.ToBase64String(bytes);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace VinaCent.Blaze.Helpers;

public static class EnumHelper
{
    public static Dictionary<string, T> ToDict<T>() where T : Enum
    {
        var type = typeof(T);
        return Enum.GetValues(type)
            .Cast<T>()
            .ToDictionary(k => k.ToString(), v => v);
    }

    public static string ToJSonObject<T>() where T : Enum
    {
        return JsonConvert.SerializeObject(ToDict<T>());
    }
}
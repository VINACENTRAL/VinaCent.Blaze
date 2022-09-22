using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public static List<T> ToList<T>() where T : Enum
    {
        var type = typeof(T);
        return Enum.GetValues(type)
            .Cast<T>()
            .ToList();
    }

    public static List<SelectListItem> ToSelectListItemst<T>(bool isTranslateEnabled = false) where T : Enum
    {
        return ToList<T>().Select(x => new SelectListItem(GetTranslate(x.ToString(), isTranslateEnabled), x.ToString())).ToList();
    }

    private static string GetTranslate(string key, bool isTranslateEnabled = false)
    {
        if (!isTranslateEnabled) return key;
        return LocalizationHelper.GetString(BlazeConsts.LocalizationSourceName, key);
    }
}
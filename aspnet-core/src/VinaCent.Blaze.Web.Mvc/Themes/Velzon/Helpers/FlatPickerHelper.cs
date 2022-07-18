using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Abp.Collections.Extensions;

namespace VinaCent.Blaze.Web.Themes.Velzon.Helpers;

// https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
// https://flatpickr.js.org/formatting/
public static class FlatPickerHelper
{
    public static Dictionary<string, string> DefaultToFlatPicker = new()
    {
        {"d", "j"},
        {"dd", "d"},
        {"ddd", "D"},
        {"dddd", "l"},
        {"m", "i"},
        {"mm", "i"},
        {"M", "n"},
        {"MM", "m"},
        {"MMM", "M"},
        {"MMMM", "F"},
        {"h", "G"},
        {"hh", "h"},
        {"H", "H"},
        {"HH", "H"},
        {"s", "s"},
        {"ss", "S"},
        {"t", "K"},
        {"tt", "K"},
        {"y", "y"},
        {"yy", "y"},
        {"yyy", "y"},
        {"yyyy", "Y"},
    };

    public static string FlatPickerShortDatePattern(this CultureInfo inp)
    {
        if (inp.Clone() is not CultureInfo clone) return string.Empty;

        return clone.DateTimeFormat.ShortDatePattern.Split(clone.DateTimeFormat.DateSeparator)
            .Select(el => DefaultToFlatPicker.FirstOrDefault(x => x.Key == el).Value ?? el)
            .JoinAsString(clone.DateTimeFormat.DateSeparator);
    }
    
    public static string FlatPickerShortTimePattern(this CultureInfo inp)
    {
        if (inp.Clone() is not CultureInfo clone) return string.Empty;

        return clone.DateTimeFormat.ShortTimePattern.Split(clone.DateTimeFormat.TimeSeparator)
            .Select(el => DefaultToFlatPicker.FirstOrDefault(x => x.Key == el).Value ?? el)
            .JoinAsString(clone.DateTimeFormat.TimeSeparator);
    }
}
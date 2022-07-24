using System.Collections.Generic;
using Abp.Localization;
using Microsoft.AspNetCore.Http;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonAccountLanguage;

public class LanguageSelectionViewModel
{
    public LanguageInfo CurrentLanguage { get; set; }

    public IReadOnlyList<LanguageInfo> Languages { get; set; }

    public PathString CurrentUrl { get; set; }
}

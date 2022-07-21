using Abp.Localization;
using System.Collections.Generic;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonLanguageSwitcher;

public class VelzonLanguageSwitcherViewComponentModel
{
    public LanguageInfo CurrentLanguage { get; set; }

    public List<LanguageInfo> OtherLanguages { get; set; }
}

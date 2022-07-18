using Abp.AspNetCore.Mvc.ViewComponents;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonLanguageSwitcher;

public class VelzonLanguageSwitcherViewComponent : AbpViewComponent
{
    private readonly ILanguageManager _languageManager;

    public VelzonLanguageSwitcherViewComponent(ILanguageManager languageManager)
    {
        _languageManager = languageManager;
    }

    public IViewComponentResult Invoke()
    {
        var currentLanguage = _languageManager.CurrentLanguage;
        var model = new VelzonLanguageSwitcherViewComponentModel
        {
            CurrentLanguage = currentLanguage,
            OtherLanguages = _languageManager.GetLanguages().Where(l => !l.IsDisabled && l.Name != currentLanguage.Name).ToList()
        };

        return View($"~/Themes/Velzon/Components/{nameof(VelzonLanguageSwitcherViewComponent).Remove("ViewComponent")}/Default.cshtml", model);
    }
}


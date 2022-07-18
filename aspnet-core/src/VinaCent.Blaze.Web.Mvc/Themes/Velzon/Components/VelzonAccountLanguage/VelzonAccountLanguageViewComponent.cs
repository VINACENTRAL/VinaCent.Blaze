using Abp.AspNetCore.Mvc.ViewComponents;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonAccountLanguage
{
    public class VelzonAccountLanguageViewComponent : AbpViewComponent
    {
        private readonly ILanguageManager _languageManager;

        public VelzonAccountLanguageViewComponent(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        public IViewComponentResult Invoke()
        {
            var currentLanguage = _languageManager.CurrentLanguage;
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = currentLanguage,
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled && l.Name != currentLanguage.Name).ToList(),
                CurrentUrl = Request.Path
            };

            return View($"~/Themes/Velzon/Components/{nameof(VelzonAccountLanguageViewComponent).Remove("ViewComponent")}/Default.cshtml", model);
        }
    }
}

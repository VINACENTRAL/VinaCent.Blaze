using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonThemeSettingPanel;

public class VelzonThemeSettingPanelViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke(string classes = "")
    {
        ViewBag.Classes = classes;
        return View($"~/Themes/Velzon/Components/{nameof(VelzonThemeSettingPanelViewComponent).Replace("ViewComponent", "")}/Default.cshtml");
    }
}


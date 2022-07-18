using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonBrand;

public class VelzonBrandViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke(bool isInnerSidebar = false, bool isShowSingleOnly = false)
    {
        ViewBag.IsInnerSidebar = isInnerSidebar;
        ViewBag.IsShowSingleOnly = isShowSingleOnly;
        return View($"~/Themes/Velzon/Components/{nameof(VelzonBrandViewComponent).Remove("ViewComponent")}/Default.cshtml");
    }
}

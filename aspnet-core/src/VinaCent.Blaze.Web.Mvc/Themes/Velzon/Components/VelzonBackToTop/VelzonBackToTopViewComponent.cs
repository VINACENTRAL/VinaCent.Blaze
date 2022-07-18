using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonBackToTop;

public class VelzonBackToTopViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke(string classes = "")
    {
        ViewBag.Classes = classes;
        return View($"~/Themes/Velzon/Components/{nameof(VelzonBackToTopViewComponent).Remove("ViewComponent")}/Default.cshtml");
    }
}


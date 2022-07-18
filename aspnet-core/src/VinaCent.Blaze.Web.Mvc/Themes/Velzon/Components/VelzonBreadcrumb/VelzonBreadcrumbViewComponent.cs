using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonBreadcrumb;

public class VelzonBreadcrumbViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke(VelzonBreadcrumbOptions options)
    {
        return View($"~/Themes/Velzon/Components/{nameof(VelzonBreadcrumbViewComponent).Remove("ViewComponent")}/Default.cshtml", options);
    }
}


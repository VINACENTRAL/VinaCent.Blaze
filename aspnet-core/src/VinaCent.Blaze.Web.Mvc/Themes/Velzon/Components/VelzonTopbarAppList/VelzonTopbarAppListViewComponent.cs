using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonTopbarAppList;

public class VelzonTopbarAppListViewComponent: AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View($"~/Themes/Velzon/Components/{nameof(VelzonTopbarAppListViewComponent).Remove("ViewComponent")}/Default.cshtml");
    }
}
using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonTopbarSearch;

public class VelzonTopbarSearchViewComponent: AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View($"~/Themes/Velzon/Components/{nameof(VelzonTopbarSearchViewComponent).Remove("ViewComponent")}/Default.cshtml");
    }
}
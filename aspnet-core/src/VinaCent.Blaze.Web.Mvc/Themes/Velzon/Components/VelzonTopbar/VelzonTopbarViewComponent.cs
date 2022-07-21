using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;
using Abp.AspNetCore.Mvc.ViewComponents;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonTopbar;

public class VelzonTopbarViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View($"~/Themes/Velzon/Components/{nameof(VelzonTopbarViewComponent).Remove("ViewComponent")}/Default.cshtml");
    }
}


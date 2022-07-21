using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonUserArea;

public class VelzonUserAreaViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View($"~/Themes/Velzon/Components/{nameof(VelzonUserAreaViewComponent).Replace("ViewComponent", "")}/Default.cshtml");
    }
}


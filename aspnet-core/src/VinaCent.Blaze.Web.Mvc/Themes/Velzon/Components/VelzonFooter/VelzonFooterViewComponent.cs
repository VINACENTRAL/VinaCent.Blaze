using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonFooter;

public class VelzonFooterViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke(string classes = "")
    {
        ViewBag.Classes = classes;
        return View($"~/Themes/Velzon/Components/{nameof(VelzonFooterViewComponent).Remove("ViewComponent")}/Default.cshtml");
    }
}


using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;
using VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonFooter.Enums;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonFooter;

public class VelzonFooterViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke(FooterTypes type = FooterTypes.SpaceBetween, string classes = "")
    {
        ViewBag.Classes = classes;
        ViewBag.FooterType = type;
        return View($"~/Themes/Velzon/Components/{nameof(VelzonFooterViewComponent).Remove("ViewComponent")}/Default.cshtml");
    }
}


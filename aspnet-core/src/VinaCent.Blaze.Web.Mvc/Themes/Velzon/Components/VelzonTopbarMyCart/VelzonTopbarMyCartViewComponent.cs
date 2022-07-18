using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonTopbarMyCart;

public class VelzonTopbarMyCartViewComponent: AbpViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(
            $"~/Themes/Velzon/Components/{nameof(VelzonTopbarMyCartViewComponent).Remove("ViewComponent")}/Default.cshtml");
    }
}
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.BusinessCP.Controllers.ShopModule;

[AbpMvcAuthorize(PermissionNames.Pages_Shop)]
[Area(nameof(BusinessCP))]
[Route("businesscp/shop/products")]
public class ProductsController: BlazeControllerBase
{
    private const string IndexView = "~/Areas/BusinessCP/Views/ShopModule/Products/Index.cshtml";

    public IActionResult Index() => View(IndexView);
}
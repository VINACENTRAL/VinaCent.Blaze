using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.BusinessCP.Controllers.ShopModule;

[AbpMvcAuthorize]
[Area(nameof(BusinessCP))]
[Route("businesscp/shop/tags")]
public class TagsController : BlazeControllerBase
{
    private const string IndexView = "~/Areas/BusinessCP/Views/ShopModule/Tags/Index.cshtml";

    public IActionResult Index() => View(IndexView);
}
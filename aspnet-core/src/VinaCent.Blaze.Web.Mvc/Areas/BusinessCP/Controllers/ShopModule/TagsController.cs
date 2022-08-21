using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.BusinessCore.ShopModule.Tags;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.BusinessCP.Controllers.ShopModule;

[AbpMvcAuthorize(PermissionNames.Pages_Shop)]
[Area(nameof(BusinessCP))]
[Route("businesscp/shop/tags")]
public class TagsController : BlazeControllerBase
{
    private const string IndexView = "~/Areas/BusinessCP/Views/ShopModule/Tags/Index.cshtml";
    private const string EditView = "~/Areas/BusinessCP/Views/ShopModule/Tags/_EditModal.cshtml";

    private readonly IShopTagAppService _shopTagAppService;

    public TagsController(IShopTagAppService shopTagAppService)
    {
        _shopTagAppService = shopTagAppService;
    }

    public IActionResult Index() => View(IndexView);

    [HttpPost("edit-modal")]
    public async Task<ActionResult> EditModal(Guid id)
    {

        var model = await _shopTagAppService.GetAsync(new EntityDto<Guid>(id));
        return PartialView(EditView, model);
    }

}
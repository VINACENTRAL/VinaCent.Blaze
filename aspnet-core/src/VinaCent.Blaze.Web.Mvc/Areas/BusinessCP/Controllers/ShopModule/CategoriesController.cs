using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.BusinessCore.ShopModule.Categories;
using VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;
using VinaCent.Blaze.Controllers;
using VinaCent.Blaze.Web.Areas.BusinessCP.Models.ShopModule.Categories;

namespace VinaCent.Blaze.Web.Areas.BusinessCP.Controllers.ShopModule;

[AbpMvcAuthorize(PermissionNames.Pages_Shop)]
[Area(nameof(BusinessCP))]
[Route("businesscp/shop/categories")]
public class CategoriesController : BlazeControllerBase
{
    private const string IndexView = "~/Areas/BusinessCP/Views/ShopModule/Categories/Index.cshtml";
    private const string EditView = "~/Areas/BusinessCP/Views/ShopModule/Categories/_EditModal.cshtml";

    private readonly IShopCategoryAppService _shopCategoryAppService;

    public CategoriesController(IShopCategoryAppService shopCategoryAppService)
    {
        _shopCategoryAppService = shopCategoryAppService;
    }

    public IActionResult Index() => View(IndexView, new CategoryViewModel
    {
        CurrentLevel = 0 // Parent
    });

    [HttpGet("children")]
    public async Task<IActionResult> Children()
    {
        await GetParentCategoryListAsync();
        return View(IndexView, new CategoryViewModel { CurrentLevel = 1 });
    }

    [HttpPost("edit-modal")]
    public async Task<ActionResult> EditModal(int id)
    {
        await GetParentCategoryListAsync();
        var dto = await _shopCategoryAppService.GetAsync(new EntityDto(id));
        var model = ObjectMapper.Map<UpdateCategoryDto>(dto);
        return PartialView(EditView, model);
    }

    private async Task GetParentCategoryListAsync()
    {
        var parents = await _shopCategoryAppService.GetAllListByLevelAsync(0);
        ViewBag.Parents = parents.Select(x=> new SelectListItem(x.Title, x.Id.ToString()));
    }
}
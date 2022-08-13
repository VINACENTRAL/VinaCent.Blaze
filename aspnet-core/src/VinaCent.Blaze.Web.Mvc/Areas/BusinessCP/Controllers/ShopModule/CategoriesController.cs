using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.BusinessCore.ShopModule.Categories;
using VinaCent.Blaze.BusinessCore.ShopModule.Categories.Dto;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.BusinessCP.Controllers.ShopModule;

[AbpMvcAuthorize]
[Area(nameof(BusinessCP))]
[Route("businesscp/shop/categories")]
public class CategoriesController : BlazeControllerBase
{
    private const string IndexView = "~/Areas/BusinessCP/Views/ShopModule/Categories/Index.cshtml";
    private const string ChildrenView = "~/Areas/BusinessCP/Views/ShopModule/Categories/Children.cshtml";
    private const string EditView = "~/Areas/BusinessCP/Views/ShopModule/Categories/_EditModal.cshtml";

    private readonly IShopCategoryAppService _shopCategoryAppService;

    public CategoriesController(IShopCategoryAppService shopCategoryAppService)
    {
        _shopCategoryAppService = shopCategoryAppService;
    }

    public IActionResult Index() => View(IndexView);
    
    [HttpGet("children")]
    public IActionResult Children() => View(ChildrenView);
    
    [HttpPost("edit-modal")]
    public async Task<ActionResult> EditModal(int id)
    {
        var dto = await _shopCategoryAppService.GetAsync(new EntityDto(id));
        var model = ObjectMapper.Map<UpdateCategoryDto>(dto);
        return PartialView(EditView, model);
    }
}
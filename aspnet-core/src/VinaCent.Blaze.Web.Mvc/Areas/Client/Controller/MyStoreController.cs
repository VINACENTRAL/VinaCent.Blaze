using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.BusinessCore.CurrencyUnits;
using VinaCent.Blaze.BusinessCore.ShopModule.Categories;
using VinaCent.Blaze.BusinessCore.ShopModule.Products.Dto;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.Client.Controller;

[Area("Client")]
[Route("/shop/my-store")]
public class MyStoreController : BlazeControllerBase
{
    private readonly IShopCategoryAppService _shopCategoryAppService;
    private readonly ICurrencyUnitAppService _currencyUnitAppService;

    public MyStoreController(IShopCategoryAppService shopCategoryAppService,
        ICurrencyUnitAppService currencyUnitAppService)
    {
        _shopCategoryAppService = shopCategoryAppService;
        _currencyUnitAppService = currencyUnitAppService;
    }

    [HttpGet("add-product")]
    public async Task<ActionResult> AddProduct()
    {
        var items = await _shopCategoryAppService.GetAllListItems();
        ViewBag.CategoryItems = items.Select(x => new SelectListItem(x.Title, x.Id.ToString()));

        ViewBag.DefaultCurrencyUnit = await _currencyUnitAppService.GetDefault();

        var viewModel = new CreateOrUpdateProductDto();

        return View(viewModel);
    }
}
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.BusinessCore.CurrencyUnits;
using VinaCent.Blaze.Controllers;
using VinaCent.Blaze.Web.Areas.BusinessCP.Models.CurrencyExchangeRateManagement;

namespace VinaCent.Blaze.Web.Areas.BusinessCP.Controllers;

[AbpMvcAuthorize(PermissionNames.Pages_CurrencyManagement)]
[Area(nameof(BusinessCP))]
[Route("businesscp/currency-exchange-rates")]
public class CurrencyExchangeRateManagementController : BlazeControllerBase
{
    private readonly ICurrencyUnitAppService _currencyUnitAppService;

    public CurrencyExchangeRateManagementController(ICurrencyUnitAppService currencyUnitAppService)
    {
        _currencyUnitAppService = currencyUnitAppService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new CurrencyExchangeRateManagementViewModel
        {
            Default = await _currencyUnitAppService.GetDefault()
        };

        ViewBag.DefaultCurrency = await _currencyUnitAppService.GetDefault();

        return View(model);
    }
}
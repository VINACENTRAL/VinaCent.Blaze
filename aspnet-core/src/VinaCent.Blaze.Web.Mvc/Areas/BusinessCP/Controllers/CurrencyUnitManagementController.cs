using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.BusinessCore.CurrencyUnits;
using VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.BusinessCP.Controllers;

[AbpMvcAuthorize(PermissionNames.Pages_CurrencyManagement)]
[Area(nameof(BusinessCP))]
[Route("businesscp/currency-units")]
public class CurrencyUnitManagementController : BlazeControllerBase
{
    private readonly ICurrencyUnitAppService _currencyUnitAppService;

    public CurrencyUnitManagementController(ICurrencyUnitAppService currencyUnitAppService)
    {
        _currencyUnitAppService = currencyUnitAppService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("edit-modal")]
    public async Task<ActionResult> EditModal(Guid id)
    {
        var languageDto = await _currencyUnitAppService.GetAsync(new EntityDto<Guid>(id));
        var model = ObjectMapper.Map<UpdateCurrencyUnitDto>(languageDto);
        return PartialView("_EditModal", model);
    }
}
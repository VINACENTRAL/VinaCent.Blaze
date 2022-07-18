using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;
using VinaCent.Blaze.MultiTenancy;

namespace VinaCent.Blaze.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    [Area("AdminCP")]
    [Route("admincp/tenants")]
    public class TenantsController : BlazeControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        [HttpGet("")]
        public ActionResult Index() => View();

        [HttpPost("edit-modal")]
        public async Task<ActionResult> EditModal(int tenantId)
        {
            var tenantDto = await _tenantAppService.GetAsync(new EntityDto(tenantId));
            return PartialView("_EditModal", tenantDto);
        }
    }
}

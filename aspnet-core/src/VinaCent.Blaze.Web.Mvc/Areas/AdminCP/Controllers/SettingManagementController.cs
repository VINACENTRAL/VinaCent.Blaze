using Abp.AspNetCore.Mvc.Authorization;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.Controllers;
using VinaCent.Blaze.Web.Areas.AdminCP.Models.SettingManagement;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize]
    [Area("AdminCP")]
    [Route("admincp/settings")]
    public class SettingManagementController : BlazeControllerBase
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return NotFound();
        }

        [HttpGet("meta")]
        public IActionResult AppMeta()
        {
            var model = new AppMetaSettingModel(SettingManager);
            return View(model);
        }

        [HttpPost("meta")]
        public async Task<JsonResult> AppMeta(AppMetaSettingModel input)
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L(LKConstants.YourDataIsInvalid));
            }
            await input.Save(SettingManager, AbpSession.TenantId);
            //var path = HttpContext.Request.GetCurrentHost() + Url.Action(nameof(AppMeta), "SettingManagement");
            return Json(new AjaxResponse(input));
        }

        [HttpGet("theme")]
        public IActionResult AppTheme()
        {
            return View();
        }

        [HttpGet("system")]
        public IActionResult AppSystem()
        {
            var model = new AppSystemSettingModel(SettingManager);
            return View(model);
        }

        [HttpPost("system")]
        public async Task<JsonResult> AppSystem(AppSystemSettingModel input)
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L(LKConstants.YourDataIsInvalid));
            }
            await input.Save(SettingManager, AbpSession.TenantId);
            //var path = HttpContext.Request.GetCurrentHost() + Url.Action(nameof(AppMeta), "SettingManagement");
            return Json(new AjaxResponse(input));
        }
    }
}

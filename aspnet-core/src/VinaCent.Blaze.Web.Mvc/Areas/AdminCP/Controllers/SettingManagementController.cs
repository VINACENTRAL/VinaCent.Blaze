using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Controllers;

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
            return View();
        }

        [HttpGet("theme")]
        public IActionResult AppTheme()
        {
            return View();
        }

        [HttpGet("system")]
        public IActionResult AppSystem()
        {
            return View();
        }
    }
}

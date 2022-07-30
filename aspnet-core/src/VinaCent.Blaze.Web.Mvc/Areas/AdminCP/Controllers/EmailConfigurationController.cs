using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize]
    [Area("AdminCP")]
    [Route("admincp/email-configuration")]
    public class EmailConfigurationController : BlazeControllerBase
    {
        public IActionResult Index() => NotFound();

        [HttpGet("mail-server")]
        public IActionResult SetUpMailServer()
        {
            return View();
        }

        [HttpGet("text-templates")]
        public IActionResult TextTemplates()
        {
            return View();
        }
    }
}

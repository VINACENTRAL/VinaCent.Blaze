using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Languages)]
    [Area("AdminCP")]
    [Route("admincp/languages")]
    public class LanguagesController : BlazeControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_LanguageTexts)]
    [Area("AdminCP")]
    [Route("admincp/language-texts")]
    public class LanguageTextsController : BlazeControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

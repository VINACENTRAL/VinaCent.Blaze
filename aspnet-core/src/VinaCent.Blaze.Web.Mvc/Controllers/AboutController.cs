using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : BlazeControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}

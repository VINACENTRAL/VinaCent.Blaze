using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.Client.Controller
{
    [AllowAnonymous]
    [Area("Client")]
    [Route("/")]
    public class HomeController : BlazeControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

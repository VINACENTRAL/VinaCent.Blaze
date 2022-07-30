using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Controllers
{
    [Route("profile")]
    [AbpAuthorize]
    public class ProfileController : BlazeControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("settings")]
        public async Task<IActionResult> Settings()
        {
            return View();
        }
    }
}

using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.AppCore.UserLoginAttempts;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize]
    [Area("AdminCP")]
    [Route("admincp/security-logs")]
    public class SecurityLogsController : BlazeControllerBase
    {
        private readonly IUserLoginAttemptAppService _userLoginAttemptAppService;
        public SecurityLogsController(IUserLoginAttemptAppService userLoginAttemptAppService)
        {
            _userLoginAttemptAppService = userLoginAttemptAppService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

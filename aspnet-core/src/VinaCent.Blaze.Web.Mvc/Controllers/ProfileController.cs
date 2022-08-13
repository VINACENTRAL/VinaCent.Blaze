using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.Controllers;
using VinaCent.Blaze.Profiles;
using VinaCent.Blaze.Profiles.Dto;
using VinaCent.Blaze.Web.Contributors.ProfileManagement;
using VinaCent.Blaze.Web.Models.Profile;

namespace VinaCent.Blaze.Web.Controllers
{
    [Route("profile")]
    [AbpAuthorize]
    public class ProfileController : BlazeControllerBase
    {
        protected ProfileManagementPageOptions Options { get; }
        public IServiceProvider ServiceProvider { get; set; }
        public IProfileAppService _profileAppService;

        public ProfileController(IOptions<ProfileManagementPageOptions> options,
            IServiceProvider serviceProvider,
            IProfileAppService profileAppService)
        {
            Options = options.Value;
            ServiceProvider = serviceProvider;
            _profileAppService = profileAppService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var model = new ProfilePageModel
            {
                ProfileManagementPageCreationContext = new ProfileManagementPageCreationContext(ServiceProvider)
            };

            foreach (var contributor in Options.Contributors)
            {
                await contributor.ConfigureAsync(model.ProfileManagementPageCreationContext);
            }

            return View(model);
        }

        [HttpPost("send-code")]
        public async Task<ActionResult> SendCode(string emailAddress)
        {
            var token = await _profileAppService.SendConfirmCodeAsync(new RequestEmailDto { Email = emailAddress });
           
            return Json(token);
        }

    }
}

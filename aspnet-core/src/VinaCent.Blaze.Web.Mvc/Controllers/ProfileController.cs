using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.Controllers;
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

        public ProfileController(IOptions<ProfileManagementPageOptions> options, 
            IServiceProvider serviceProvider)
        {
            Options = options.Value;
            ServiceProvider = serviceProvider;
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
    }
}

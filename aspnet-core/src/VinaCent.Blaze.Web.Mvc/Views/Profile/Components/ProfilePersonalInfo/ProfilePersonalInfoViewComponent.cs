using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.Profiles;

namespace VinaCent.Blaze.Web.Views.Profile.Components.ProfilePersonalInfo
{
    public class ProfilePersonalInfoViewComponent : BlazeViewComponent
    {
        protected IProfileAppService ProfileAppService { get; }

        public ProfilePersonalInfoViewComponent(IProfileAppService profileAppService)
        {
            ProfileAppService = profileAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await ProfileAppService.GetAsync();
            return View("~/Views/Profile/Components/ProfilePersonalInfo/Default.cshtml", user);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.Profiles;
using VinaCent.Blaze.Web.Models.Profile;

namespace VinaCent.Blaze.Web.Views.Profile.Components.ProfilePassword
{
    public class ProfilePasswordViewComponent : BlazeViewComponent
    {
        protected IProfileAppService ProfileAppService { get; }

        public ProfilePasswordViewComponent(IProfileAppService profileAppService)
        {
            ProfileAppService = profileAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await ProfileAppService.GetAsync();

            var model = new ChangePasswordModel
            {
                HideOldPasswordInput = !user.HasPassword
            };
            return View("~/Views/Profile/Components/ProfilePassword/Default.cshtml", model);
        }
    }
}

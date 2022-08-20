using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.Profiles;
using VinaCent.Blaze.Profiles.Dto;

namespace VinaCent.Blaze.Web.Views.Profile.Components.ProfileSocial
{
    public class ProfileSocialViewComponent : BlazeViewComponent
    {
        private readonly IProfileAppService _profileAppService;

        public ProfileSocialViewComponent(IProfileAppService profileAppService)
        {
            _profileAppService = profileAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _profileAppService.GetAsync();

            var model = new UpdateSocialDto
            {
                ListSocialNetworkRawJson = user.ListSocialNetworkRawJson
            };

            return View("~/Views/Profile/Components/ProfileSocial/Default.cshtml", model);
        }
    }
}

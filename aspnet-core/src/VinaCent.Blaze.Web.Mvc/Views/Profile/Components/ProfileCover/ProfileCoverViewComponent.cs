using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.Profiles;
using VinaCent.Blaze.Profiles.Dto;

namespace VinaCent.Blaze.Web.Views.Profile.Components.ProfileCover
{
    public class ProfileCoverViewComponent : BlazeViewComponent
    {
        private readonly IProfileAppService _profileAppService;

        public ProfileCoverViewComponent(IProfileAppService profileAppService)
        {
            _profileAppService = profileAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _profileAppService.GetAsync();
            var model = new UpdateCoverDto
            {
                Cover = user.Cover
            };

            return View("~/Views/Profile/Components/ProfileCover/Default.cshtml", model);
        }
    }
}

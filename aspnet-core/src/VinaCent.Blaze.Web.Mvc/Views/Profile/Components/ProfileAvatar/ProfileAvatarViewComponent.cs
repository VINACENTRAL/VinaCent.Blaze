using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Profiles;
using VinaCent.Blaze.Profiles.Dto;

namespace VinaCent.Blaze.Web.Views.Profile.Components.ProfileAvatar;

public class ProfileAvatarViewComponent : BlazeViewComponent
{
    private readonly IProfileAppService _profileAppService;

    public ProfileAvatarViewComponent(IProfileAppService profileAppService)
    {
        _profileAppService = profileAppService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _profileAppService.GetAsync();
        var model = new UpdateAvatarDto
        {
            Avatar = user.Avatar
        };

        ViewData["FullName"] = user.FullName;
        return View("~/Views/Profile/Components/ProfileAvatar/Default.cshtml", model);
    }
}
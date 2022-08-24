using VinaCent.Blaze.Profiles.Dto;
using VinaCent.Blaze.Web.Contributors.ProfileManagement;

namespace VinaCent.Blaze.Web.Models.Profile
{
    public class ProfilePageModel
    {
        public ProfileManagementPageCreationContext ProfileManagementPageCreationContext { get; set; }

        public ProfileDto CurrentUser { get; set; }
    }
}

using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Profiles.Dto
{
    public class ChangePasswordDto
    {
        [AppRequired]
        public string CurrentPassword { get; set; }

        [AppRequired]
        public string NewPassword { get; set; }
    }
}

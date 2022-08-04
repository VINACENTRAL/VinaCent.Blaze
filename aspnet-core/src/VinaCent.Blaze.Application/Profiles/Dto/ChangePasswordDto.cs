using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.Profiles.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}

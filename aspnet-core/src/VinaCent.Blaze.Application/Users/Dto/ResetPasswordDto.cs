using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Users.Dto
{
    public class ResetPasswordDto
    {
        [AppRequired]
        public string AdminPassword { get; set; }

        [AppRequired]
        public long UserId { get; set; }

        [AppRequired]
        public string NewPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.Common;

namespace VinaCent.Blaze.Authorization.Accounts.Dto
{
    public class ResetPasswordInput
    {
        [Required]
        [RegularExpression(AvailableRegexs.EmailChecker)]
        public string EmailAddress { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [RegularExpression(AvailableRegexs.PasswordRegex)]
        public string NewPassword { get; set; }
    }
}

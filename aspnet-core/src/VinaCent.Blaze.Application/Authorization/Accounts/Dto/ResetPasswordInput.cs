using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.Common;

namespace VinaCent.Blaze.Authorization.Accounts.Dto
{
    public class ResetPasswordInput
    {
        [Required]
        [RegularExpression(AvailableRegexs.EmailChecker)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Token)]
        public string Token { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.NewPassword)]
        [RegularExpression(AvailableRegexs.PasswordRegex)]
        public string NewPassword { get; set; }
    }
}

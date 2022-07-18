using Abp.Localization;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Authorization.Accounts.Dto
{
    public class ResetPasswordInput
    {
        [AppRequired]
        [AppRegex(RegexLib.EmailChecker)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EmailAddress)]
        public string EmailAddress { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Token)]
        public string Token { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.NewPassword)]
        [AppRegex(RegexLib.PasswordRegex)]
        public string NewPassword { get; set; }
    }
}

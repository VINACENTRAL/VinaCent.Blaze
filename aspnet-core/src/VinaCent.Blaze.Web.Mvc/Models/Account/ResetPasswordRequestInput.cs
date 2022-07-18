using Abp.Localization;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Web.Models.Account
{
    public class ResetPasswordRequestInput
    {
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EmailAddress)]
        [AppRegex(RegexLib.EmailChecker)]
        public string EmailAddress { get; set; }
    }
}

using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Profiles.Dto
{
    public class ChangeEmailDto
    {
        [AppRequired]
        public string VerifyToken { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.NewEmail)]
        [AppRegex(RegexLib.EmailChecker)]
        public string NewEmail { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ConfirmCode)]
        public string ConfirmCode { get; set; }
    }
}

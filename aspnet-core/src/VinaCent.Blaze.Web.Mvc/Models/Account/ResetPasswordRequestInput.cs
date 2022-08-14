using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.Common;

namespace VinaCent.Blaze.Web.Models.Account
{
    public class ResetPasswordRequestInput
    {
        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EmailAddress)]
        [RegularExpression(RegexLib.EmailChecker)]
        public string EmailAddress { get; set; }
    }
}

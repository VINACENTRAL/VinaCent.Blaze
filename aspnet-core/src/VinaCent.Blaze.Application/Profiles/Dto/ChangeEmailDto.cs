using Abp.Localization;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.Profiles.Dto
{
    public class ChangeEmailDto
    {
        [Required]
        public string VerifyToken { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.NewEmail)]
        public string NewEmail { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ConfirmCode)]
        public string ConfirmCode { get; set; }
    }
}

using Abp.Localization;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.Profiles.Dto
{
    public class ConfirmCodeDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ConfirmCode)]
        public string ConfirmCode { get; set; }
    }
}

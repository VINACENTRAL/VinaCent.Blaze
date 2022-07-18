using Abp.Localization;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Profiles.Dto
{
    public class ConfirmCodeDto
    {
        [AppRequired]
        public string Token { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ConfirmCode)]
        public string ConfirmCode { get; set; }
    }
}

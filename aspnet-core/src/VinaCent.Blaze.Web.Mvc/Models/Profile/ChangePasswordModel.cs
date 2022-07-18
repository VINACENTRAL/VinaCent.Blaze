using Abp.Auditing;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Web.Models.Profile
{
    public class ChangePasswordModel
    {
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrentPassword)]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string CurrentPassword { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.NewPassword)]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string NewPassword { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.NewPasswordConfirm)]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string NewPasswordConfirm { get; set; }

        public bool HideOldPasswordInput { get; set; }
    }
}

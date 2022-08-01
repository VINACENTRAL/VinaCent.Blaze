using Abp.Localization;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.EmailConfiguration.SetUpMailServer
{
    public class SetupEmailerDto
    {
        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.DefaultFromAddress)]
        public string DefaultFromAddress { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.DefaultFromDisplayName)]
        public string DefaultFromDisplayName { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpHost)]
        public string SmtpHost { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpPort)]
        public string SmtpPort { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpUserName)]
        public string SmtpUserName { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpPassword)]
        public string SmtpPassword { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpDomain)]
        public string SmtpDomain { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpEnableSsl)]
        public bool SmtpEnableSsl { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpUseDefaultCredentials)]
        public bool SmtpUseDefaultCredentials { get; set; }
    }
}

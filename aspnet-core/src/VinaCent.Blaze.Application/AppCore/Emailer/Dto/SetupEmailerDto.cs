using Abp.Localization;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.EmailConfiguration.SetUpMailServer
{
    public class SetupEmailerDto
    {
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.DefaultFromAddress)]
        public string DefaultFromAddress { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.DefaultFromDisplayName)]
        public string DefaultFromDisplayName { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpHost)]
        public string SmtpHost { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpPort)]
        public string SmtpPort { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpUserName)]
        public string SmtpUserName { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpPassword)]
        public string SmtpPassword { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpDomain)]
        public string SmtpDomain { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpEnableSsl)]
        public bool SmtpEnableSsl { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SmtpUseDefaultCredentials)]
        public bool SmtpUseDefaultCredentials { get; set; }
    }
}

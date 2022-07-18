using Abp.Localization;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.EmailConfiguration.SetUpMailServer
{
    public class TestEmailSenderDto
    {
        [AppRegex(RegexLib.EmailListSeprateByCommaChecker)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Receivers)]
        public string Receivers { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
        public string Content { get; set; }
    }
}

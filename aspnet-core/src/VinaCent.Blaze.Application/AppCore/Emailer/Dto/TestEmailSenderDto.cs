using Abp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.Common;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.EmailConfiguration.SetUpMailServer
{
    public class TestEmailSenderDto
    {
        [RegularExpression(AvailableRegexs.EmailListSeprateByCommaChecker,
            ErrorMessage = "Invalid. Ex: email1@vinacent.com, email2@vinacent.com")]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Receivers)]
        public string Receivers { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
        public string Content { get; set; }
    }
}

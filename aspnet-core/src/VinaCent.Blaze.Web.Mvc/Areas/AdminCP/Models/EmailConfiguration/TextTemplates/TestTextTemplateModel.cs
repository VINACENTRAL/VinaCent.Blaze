using Abp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.EmailConfiguration.TextTemplates
{
    public class TestTextTemplateModel
    {
        public Guid TextTemplateId { get; set; }

        /// <summary>
        /// Email address of revicers, split by comma ','
        /// </summary>
        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName ,LKConstants.Receivers)]
        public string Receivers { get; set; }

        /// <summary>
        /// Parameter will replace to email body {0}, {1},...{n}. Split by new line, per parameter on a line.
        /// </summary>
        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Parameters)]
        public string Parameters { get; set; }
    }
}

using Abp.AutoMapper;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.AppCore.TextTemplates.Dto
{
    [AutoMapTo(typeof(TextTemplate))]
    public class CreateTextTemplateDto
    {
        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Name)]
        public string Name { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
        public string Content { get; set; }
    }
}

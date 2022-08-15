using Abp.AutoMapper;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.TextTemplates.Dto
{
    [AutoMapTo(typeof(TextTemplate))]
    public class CreateTextTemplateDto
    {
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Name)]
        public string Name { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
        public string Content { get; set; }
    }
}

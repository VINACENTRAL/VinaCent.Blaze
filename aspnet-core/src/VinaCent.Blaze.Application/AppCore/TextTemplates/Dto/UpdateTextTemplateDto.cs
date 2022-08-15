using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.TextTemplates.Dto
{
    [AutoMapFrom(typeof(TextTemplateDto))]
    [AutoMapTo(typeof(TextTemplate))]
    public class UpdateTextTemplateDto : EntityDto<Guid>
    {
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Name)]
        public string Name { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Content)]
        public string Content { get; set; }
    }
}

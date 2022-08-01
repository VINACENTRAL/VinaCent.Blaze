using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace VinaCent.Blaze.AppCore.TextTemplates.Dto
{
    [AutoMapFrom(typeof(TextTemplate))]
    public class TextTemplateListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public bool IsStatic { get; set; }
    }
}

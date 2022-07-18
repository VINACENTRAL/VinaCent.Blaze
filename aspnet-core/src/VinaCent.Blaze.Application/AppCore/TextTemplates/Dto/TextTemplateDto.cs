using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using System;

namespace VinaCent.Blaze.AppCore.TextTemplates.Dto
{
    [AutoMapFrom(typeof(TextTemplate))]
    public class TextTemplateDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsStatic { get; set; }

        public string Apply(params string[] parameters)
        {
            if (parameters.IsNullOrEmpty()) return Content;

            var content = Content;
            for (int i = 0; i < parameters.Length; i++)
            {
                content = content.Replace("{{" + i + "}}", parameters[i]);
            }
            return content;
        }
    }
}

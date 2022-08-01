using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.AppCore.TextTemplates.Dto
{
    public class PagedTextTemplateResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

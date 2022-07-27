using Abp.Application.Services.Dto;
using Abp.Localization;

namespace VinaCent.Blaze.AppCore.LanguageTexts.Dto
{
    public class PagedLanguageTextResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string DefaultLanguageName { get; set; }
        public string SourceName { get; set; }
        public string CurrentLanguageName { get; set; }
    }
}

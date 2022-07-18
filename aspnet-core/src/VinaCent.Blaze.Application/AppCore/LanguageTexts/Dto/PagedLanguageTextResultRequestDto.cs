using Abp.Application.Services.Dto;
using Abp.Localization;

namespace VinaCent.Blaze.AppCore.LanguageTexts.Dto
{
    public class PagedLanguageTextResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.DefaultLanguageName)]
        public string DefaultLanguageName { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SourceName)]
        public string SourceName { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrentLanguageName)]
        public string CurrentLanguageName { get; set; }
    }
}

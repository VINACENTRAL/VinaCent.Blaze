using Abp.Application.Services.Dto;
using Abp.Localization;

namespace VinaCent.Blaze.AppCore.Languages.Dto
{
    public class PagedLanguageResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        /// <summary>
        /// Is this language active. Inactive languages are not get by <see cref="IApplicationLanguageManager"/>.
        /// Null for all result set
        /// </summary>
        public bool? IsDisabled { get; set; }
    }
}

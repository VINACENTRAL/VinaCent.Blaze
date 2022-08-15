using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.LanguageTexts.Dto
{
    [AutoMapTo(typeof(ApplicationLanguageText))]
    public class CreateLanguageTextDto : IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// Language name (culture name). Matches to <see cref="ApplicationLanguage.Name"/>.
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguage.MaxNameLength)]
        public string LanguageName { get; set; }

        /// <summary>
        /// Localization source name
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguageText.MaxSourceNameLength)]
        public string Source { get; set; }

        /// <summary>
        /// Localization key
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguageText.MaxKeyLength)]
        public string Key { get; set; }

        /// <summary>
        /// Localized value
        /// </summary>
        [AppRequired(AllowEmptyStrings = true)]
        [AppStringLength(ApplicationLanguageText.MaxValueLength)]
        public string Value { get; set; }
    }
}

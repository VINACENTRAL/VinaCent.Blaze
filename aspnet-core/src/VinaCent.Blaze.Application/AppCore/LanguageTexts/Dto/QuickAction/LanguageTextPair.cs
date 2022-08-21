using Abp.AutoMapper;
using Abp.Localization;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.LanguageTexts.Dto.QuickAction
{
    [AutoMap(typeof(ApplicationLanguageText))]
    public class LanguageTextPair
    {
        /// <summary>
        /// Language name (culture name). Matches to <see cref="ApplicationLanguage.Name"/>.
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguage.MaxNameLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.LanguageName)]
        public string LanguageName { get; set; }

        /// <summary>
        /// Localized value
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguageText.MaxValueLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Value)]
        public string Value { get; set; }
    }
}

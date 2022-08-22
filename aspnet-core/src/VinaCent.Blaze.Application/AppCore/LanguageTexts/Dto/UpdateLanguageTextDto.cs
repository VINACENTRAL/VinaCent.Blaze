using Abp.Domain.Entities;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.LanguageTexts.Dto
{
    [AutoMap(typeof(ApplicationLanguageText))]
    [AutoMapFrom(typeof(LanguageTextDto))]
    public class UpdateLanguageTextDto : EntityDto<long>, IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.TenantId)]
        public int? TenantId { get; set; }

        /// <summary>
        /// Language name (culture name). Matches to <see cref="ApplicationLanguage.Name"/>.
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguage.MaxNameLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.LanguageName)]
        public string LanguageName { get; set; }

        /// <summary>
        /// Localization source name
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguageText.MaxSourceNameLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SourceName)]
        public string Source { get; set; }

        /// <summary>
        /// Localization key
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguageText.MaxKeyLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Key)]
        public string Key { get; set; }

        /// <summary>
        /// Localized value
        /// </summary>
        [AppRequired(AllowEmptyStrings = true)]
        [AppStringLength(ApplicationLanguageText.MaxValueLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Value)]
        public string Value { get; set; }
    }
}

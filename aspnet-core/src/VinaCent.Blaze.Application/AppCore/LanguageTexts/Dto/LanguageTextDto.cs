using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.AppCore.LanguageTexts.Dto
{
    public class LanguageTextDto : AuditedEntityDto<long>, IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// Language name (culture name). Matches to <see cref="ApplicationLanguage.Name"/>.
        /// </summary>
        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength)]
        public string LanguageName { get; set; }

        /// <summary>
        /// Localization source name
        /// </summary>
        [Required]
        [StringLength(ApplicationLanguageText.MaxSourceNameLength)]
        public string Source { get; set; }

        /// <summary>
        /// Localization key
        /// </summary>
        [Required]
        [StringLength(ApplicationLanguageText.MaxKeyLength)]
        public string Key { get; set; }

        /// <summary>
        /// Localized value
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [StringLength(ApplicationLanguageText.MaxValueLength)]
        public string Value { get; set; }
    }
}

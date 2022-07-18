using Abp.AutoMapper;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.AppCore.TranslateFields
{
    [AutoMapTo(typeof(TranslatedField))]
    public class TranslateField
    {
        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.LanguageName)]
        public string LanguageName { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EntityName)]
        public string EntityName { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EntityId)]
        public string EntityId { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.FieldName)]
        public string FieldName { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.LanguageText)]
        public string LanguageText { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IsIgnore)]
        public bool IsIgnore { get; set; }
    }
}

using Abp.AutoMapper;
using Abp.Localization;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.TranslateFields
{
    [AutoMapTo(typeof(TranslatedField))]
    public class TranslateField
    {
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.LanguageName)]
        public string LanguageName { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EntityName)]
        public string EntityName { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EntityId)]
        public string EntityId { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.FieldName)]
        public string FieldName { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.LanguageText)]
        public string LanguageText { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IsIgnore)]
        public bool IsIgnore { get; set; }
    }
}

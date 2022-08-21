using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using System.Collections.Generic;
using VinaCent.Blaze.AppCore.LanguageTexts.Dto.QuickAction;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.LanguageTexts.Dto
{
    public class GroupLanguageTextRequestInput : IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.TenantId)]
        public int? TenantId { get; set; }

        /// <summary>
        /// Localization key
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Key)]
        public string Key { get; set; }

        /// <summary>
        /// Localization source name
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SourceName)]
        public string Source { get; set; }


        public List<LanguageTextPair> Pairs { get; set; }
    }
}

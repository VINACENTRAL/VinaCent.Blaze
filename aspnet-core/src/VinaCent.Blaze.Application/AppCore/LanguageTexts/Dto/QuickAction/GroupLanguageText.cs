using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using System.Collections.Generic;
using VinaCent.Blaze.AppCore.LanguageTexts.Dto.QuickAction;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.LanguageTexts.Dto
{
    [AutoMap(typeof(ApplicationLanguageText))]
    public class GroupLanguageText : IMayHaveTenant
    {
        /// <summary>
        /// Mã bản dịnh được nhấn khi thực hiện thao tác, dùng để lấy các thông tin liên quan trước khi cập nhật
        /// </summary>
        public long? RefLanguageTextId { get; set; }

        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.TenantId)]
        public int? TenantId { get; set; }

        /// <summary>
        /// Localization key
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguageText.MaxKeyLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Key)]
        public string Key { get; set; }

        /// <summary>
        /// Localization source name
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguageText.MaxSourceNameLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SourceName)]
        public string Source { get; set; }


        public List<LanguageTextPair> Pairs { get; set; }
    }
}

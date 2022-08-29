using Abp.AutoMapper;
using Abp.Localization;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.CommonDatas.Dto
{
    [AutoMap(typeof(CommonData))]
    public class CreateCommonDataDto
    {
        /// <summary>
        /// Khóa giá trị
        /// </summary>
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Key)]
        public string Key { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Value)]
        public string Value { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Description)]
        public string Description { get; set; }

        /// <summary>
        /// Loại giá trị
        /// </summary>
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Type)]
        public string Type { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ParentKey)]
        public string ParentKey { get; set; }
    }
}

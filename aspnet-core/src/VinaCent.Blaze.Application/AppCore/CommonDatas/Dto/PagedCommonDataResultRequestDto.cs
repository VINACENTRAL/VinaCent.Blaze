using Abp.Application.Services.Dto;
using Abp.Localization;

namespace VinaCent.Blaze.AppCore.CommonDatas.Dto
{
    public class PagedCommonDataResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        /// <summary>
        /// Loại giá trị
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Type)]
        public string Type { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ParentKey)]
        public string ParentKey { get; set; }
    }
}

using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.AppCore.CommonDatas.Dto
{
    public class PagedCommonDataResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        /// <summary>
        /// Loại giá trị
        /// </summary>
        public string Type { get; set; }

        public string ParentKey { get; set; }
    }
}

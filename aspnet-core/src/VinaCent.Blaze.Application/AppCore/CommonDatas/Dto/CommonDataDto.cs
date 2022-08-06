using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace VinaCent.Blaze.AppCore.CommonDatas.Dto
{
    [AutoMap(typeof(CommonData))]
    public class CommonDataDto : EntityDto<Guid>
    {
        /// <summary>
        /// Khóa giá trị
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Loại giá trị
        /// </summary>
        public string Type { get; set; }

        public string ParentKey { get; set; }
    }
}

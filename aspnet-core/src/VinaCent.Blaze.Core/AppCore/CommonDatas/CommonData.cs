using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.AppCore.CommonDatas
{
    [Table(nameof(AppCore) + "." + nameof(CommonData))]
    public class CommonData : Entity<Guid>
    {
        /// <summary>
        /// Từ khóa
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

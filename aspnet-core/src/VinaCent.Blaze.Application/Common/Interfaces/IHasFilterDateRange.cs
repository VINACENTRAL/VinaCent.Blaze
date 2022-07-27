using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinaCent.Blaze.Common.Interfaces
{
    /// <summary>
    /// Đánh dấu là có lọc theo khoảng thời gian
    /// </summary>
    public interface IHasFilterDateRange
    {
        /// <summary>
        /// Thời điểm bắt đầu
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// Thời điểm kết thúc
        /// </summary>
        public string EndTime { get; set; }
    }
}

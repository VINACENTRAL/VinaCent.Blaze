using System;
using System.Globalization;
using Abp.Application.Services.Dto;
using VinaCent.Blaze.Common.Interfaces;

namespace VinaCent.Blaze.AppCore.AuditLogs.Dto
{
    /// <summary>
    /// Filter cho Audit log
    /// </summary>
    public class PagedAuditLogResultRequestDto : PagedResultRequestDto, IHasFilterDateRange
    {
        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public string StartTime { get; set; } = DateTime.Now.AddMonths(-1)
            .ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public string EndTime { get; set; } =
            DateTime.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        /// <summary>
        /// Id tennant
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// Tên user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Địa chỉ Ip
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Thông tin trình duyệt
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// Thời gian thực thi nhỏ nhất
        /// </summary>
        public long? MinExecutionDuration { get; set; }

        /// <summary>
        /// Thời gian thực thi lớn nhất
        /// </summary>
        public long? MaxExecutionDuration { get; set; }

        /// <summary>
        /// Tên phương thức
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Tên service
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Có lỗi hay không
        /// </summary>
        public bool? HasException { get; set; }

        /// <summary>
        /// Tham số
        /// </summary>
        public string Parameters { get; set; }
    }
}
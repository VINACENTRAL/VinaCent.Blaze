using System;
using System.Globalization;
using Abp.Application.Services.Dto;
using Abp.Localization;
using VinaCent.Blaze.Common.Interfaces;

namespace VinaCent.Blaze.AppCore.AuditLogs.Dto
{
    /// <summary>
    /// Filter cho Audit log
    ///</summary>
    public class PagedAuditLogResultRequestDto : PagedResultRequestDto, IHasFilterDateRange
    {
        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.StartTime)]
        public string StartTime { get; set; } = DateTime.Now.AddMonths(-1)
            .ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EndTime)]
        public string EndTime { get; set; } =
            DateTime.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        /// <summary>
        /// Id tennant
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// Tên user
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.UserName)]
        public string UserName { get; set; }

        /// <summary>
        /// Địa chỉ Ip
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ClientIpAddress)]
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Thông tin trình duyệt
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.BrowserInfo)]
        public string BrowserInfo { get; set; }

        /// <summary>
        /// Thời gian thực thi nhỏ nhất
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.MinExecutionDuration)]
        public long? MinExecutionDuration { get; set; }

        /// <summary>
        /// Thời gian thực thi lớn nhất
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.MaxExecutionDuration)]
        public long? MaxExecutionDuration { get; set; }

        /// <summary>
        /// Tên phương thức
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.MethodName)]
        public string MethodName { get; set; }

        /// <summary>
        /// Tên service
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ServiceName)]
        public string ServiceName { get; set; }

        /// <summary>
        /// Có lỗi hay không
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.HasException)]
        public bool? HasException { get; set; }

        /// <summary>
        /// Tham số
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Parameters)]
        public string Parameters { get; set; }
    }
}
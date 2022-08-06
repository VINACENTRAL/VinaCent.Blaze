using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using System;
using System.Globalization;

namespace VinaCent.Blaze.AppCore.AuditLogs.Dto
{
    [AutoMap(typeof(AuditLog))]
    public class AuditLogListDto : EntityDto<long>, IMayHaveTenant
    {
        /// <summary>
        /// TenantId.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.TenantId)]
        public int? TenantId { get; set; }

        /// <summary>
        /// UserId.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.UserName)]
        public string UserName { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ServiceName)]
        public string ServiceName { get; set; }

        /// <summary>
        /// Executed method name.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.MethodName)]
        public string MethodName { get; set; }

        /// <summary>
        /// Start time of the method execution.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ExecutionTime)]
        public DateTime ExecutionTime { get; set; }

        public virtual string ExecutionTimeStr
        {
            get
            {
                var currentCultureDatetimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                var pattern = currentCultureDatetimeFormat.ShortTimePattern + " - " + currentCultureDatetimeFormat.ShortDatePattern;
                return ExecutionTime.ToString(pattern);
            }
        }

        /// <summary>
        /// Total duration of the method call as milliseconds.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ExecutionDuration)]
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ClientIpAddress)]
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ClientName)]
        public string ClientName { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.BrowserInfo)]
        public string BrowserInfo { get; set; }

        /// <summary>
        /// Store the message content of  <see cref="Exception"/>.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ExceptionMessage)]
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.HasException)]
        public bool HasException { get; set; }

        /// <summary>
        /// <see cref="AuditInfo.ImpersonatorUserId"/>.
        /// </summary>
        public long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ImpersonatorUserName)]
        public string ImpersonatorUserName { get; set; }

        /// <summary>
        /// <see cref="AuditInfo.ImpersonatorTenantId"/>.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ImpersonatorTenant)]
        public int? ImpersonatorTenantId { get; set; }
    }
}

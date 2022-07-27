using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinaCent.Blaze.AppCore.AuditLogs.Dto
{
    [AutoMap(typeof(AuditLog))]
    public class AuditLogListDto : EntityDto<long>, IMayHaveTenant
    {
        /// <summary>
        /// TenantId.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// UserId.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Executed method name.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Start time of the method execution.
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        public virtual string ExecutionTimeStr
        {
            get
            {
                var currentCultureDatetimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                var parttern = currentCultureDatetimeFormat.ShortTimePattern + " - " + currentCultureDatetimeFormat.ShortDatePattern;
                return ExecutionTime.ToString(parttern);
            }
        }

        /// <summary>
        /// Total duration of the method call as milliseconds.
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// Store the message content of  <see cref="Exception"/>.
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasException { get; set; }

        /// <summary>
        /// <see cref="AuditInfo.ImpersonatorUserId"/>.
        /// </summary>
        public long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImpersonatorUserName { get; set; }

        /// <summary>
        /// <see cref="AuditInfo.ImpersonatorTenantId"/>.
        /// </summary>
        public int? ImpersonatorTenantId { get; set; }
    }
}

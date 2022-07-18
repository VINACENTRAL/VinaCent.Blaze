using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Globalization;
using VinaCent.Blaze.Common.Interfaces;

namespace VinaCent.Blaze.AppCore.UserLoginAttempts.Dto
{
    public class PagedUserLoginAttemptResultRequestDto : PagedResultRequestDto, IMayHaveTenant, IHasFilterDateRange
    {
        /// <summary>
        /// 
        /// </summary>
        public string StartTime { get; set; } = DateTime.Now.AddMonths(-1)
            .ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        /// <summary>
        /// 
        /// </summary>
        public string EndTime { get; set; } =
            DateTime.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        /// <summary>
        /// 
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserNameOrEmailAddress { get; set; }
    }
}

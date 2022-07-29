using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Globalization;

namespace VinaCent.Blaze.AppCore.UserLoginAttempts.Dto
{
    [AutoMap(typeof(UserLoginAttempt))]
    public class UserLoginAttemptDto : EntityDto<long>
    {
        /// <summary>
        /// Tenant's Id, if Abp.Authorization.Users.UserLoginAttempt.TenancyName was a valid tenant name.
        /// </summary>  
        public int? TenantId { get; set; }

        /// <summary>
        /// Tenancy name.
        /// </summary>
        public string TenancyName { get; set; }

        /// <summary>
        /// User's Id, if Abp.Authorization.Users.UserLoginAttempt.UserNameOrEmailAddress was a valid 
        /// username or email address.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// User name or email address
        /// </summary>  
        public string UserNameOrEmailAddress { get; set; }

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
        /// Login attempt result.
        /// </summary>
        public AbpLoginResultType Result { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual string CreationTimeStr
        {
            get
            {
                var currentCultureDatetimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                var parttern = currentCultureDatetimeFormat.FullDateTimePattern + " - " + currentCultureDatetimeFormat.ShortDatePattern;
                return CreationTime.ToString(parttern);
            }
        }
    }
}

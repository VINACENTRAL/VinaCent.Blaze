using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        [AppRequired]
        [AppStringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [AppRequired]
        [AppStringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [AppRequired]
        [AppStringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [AppRequired]
        [AppRegex(RegexLib.EmailChecker)]
        [AppStringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }
        
        public bool IsEmailConfirmed { get; set; }
        
        public bool IsPhoneNumberConfirmed { get; set; }
        
        public bool IsTwoFactorEnabled { get; set; }

        public string[] RoleNames { get; set; }

        #region Profile

        [AppStringLength(50)]
        public string Country { get; set; }

        [AppStringLength(50)]
        public string State { get; set; }

        [AppStringLength(50)]
        public string City { get; set; }

        [AppStringLength(10)]
        public string ZipCode { get; set; }

        public string Description { get; set; }

        [AppStringLength(255)]
        public string Avatar { get; set; }

        [AppStringLength(255)]
        public string Background { get; set; }

        public DateTime? Birthday { get; set; }

        [AppStringLength(14)]
        public string IdentityCardNumber { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        #endregion
    }
}

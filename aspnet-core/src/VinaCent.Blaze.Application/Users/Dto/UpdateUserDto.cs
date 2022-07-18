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
    public class UpdateUserDto : EntityDto<long>
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

        public string[] RoleNames { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class CreateUserDto : IShouldNormalize
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

        [AppRequired]
        [AppStringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public void Normalize()
        {
            if (RoleNames == null)
            {
                RoleNames = new string[0];
            }
        }
    }
}

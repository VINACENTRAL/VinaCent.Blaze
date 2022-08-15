using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Extensions;
using VinaCent.Blaze.Common;
using VinaCent.Blaze.DataAnnotations;
using VinaCent.Blaze.Validation;

namespace VinaCent.Blaze.Authorization.Accounts.Dto
{
    public class RegisterInput : IValidatableObject
    {
        [AppRequired]
        [AppStringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [AppRequired]
        [AppStringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [AppRequired]
        [AppStringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [AppRequired]
        [AppRegex(RegexLib.EmailChecker)]
        [AppStringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [AppRequired]
        [AppStringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        [DisableAuditing]
        public string CaptchaResponse { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UserName.IsNullOrEmpty())
            {
                if (!UserName.Equals(EmailAddress) && ValidationHelper.IsEmail(UserName))
                {
                    yield return new ValidationResult("Username cannot be an email address unless it's the same as your email address!");
                }
            }
        }
    }
}

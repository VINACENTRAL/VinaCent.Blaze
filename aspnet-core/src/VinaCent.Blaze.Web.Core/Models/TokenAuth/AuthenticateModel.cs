using Abp.Auditing;
using Abp.Authorization.Users;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Models.TokenAuth
{
    public class AuthenticateModel
    {
        [AppRequired]
        [AppStringLength(AbpUserBase.MaxEmailAddressLength)]
        public string UserNameOrEmailAddress { get; set; }

        [AppRequired]
        [AppStringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public bool RememberClient { get; set; }
    }
}

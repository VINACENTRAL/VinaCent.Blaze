using Abp.Auditing;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Web.Models.Account
{
    public class LoginViewModel
    {
        [AppRequired]
        public string UsernameOrEmailAddress { get; set; }

        [AppRequired]
        [DisableAuditing]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

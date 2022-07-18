using Abp.MultiTenancy;

namespace VinaCent.Blaze.Web.Models.Account
{
    public class LoginFormViewModel
    {
        public string UsernameOrEmailAddress { get; set; }
        public string ReturnUrl { get; set; }

        public bool IsMultiTenancyEnabled { get; set; }

        public bool IsSelfRegistrationAllowed { get; set; }

        public MultiTenancySides MultiTenancySide { get; set; }
    }
}

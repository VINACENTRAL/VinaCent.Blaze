using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Models.TokenAuth
{
    public class ExternalAuthenticateModel
    {
        [AppRequired]
        [AppStringLength(UserLogin.MaxLoginProviderLength)]
        public string AuthProvider { get; set; }

        [AppRequired]
        [AppStringLength(UserLogin.MaxProviderKeyLength)]
        public string ProviderKey { get; set; }

        [AppRequired]
        public string ProviderAccessCode { get; set; }
    }
}

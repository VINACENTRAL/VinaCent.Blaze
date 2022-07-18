using Abp.AutoMapper;
using VinaCent.Blaze.Authentication.External;

namespace VinaCent.Blaze.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}

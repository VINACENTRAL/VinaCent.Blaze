using Abp.MultiTenancy;
using VinaCent.Blaze.Authorization.Users;

namespace VinaCent.Blaze.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}

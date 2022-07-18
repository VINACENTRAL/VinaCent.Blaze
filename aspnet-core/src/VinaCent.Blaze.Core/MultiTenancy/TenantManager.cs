using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Editions;

namespace VinaCent.Blaze.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}

using Abp.AutoMapper;
using Abp.MailKit;
using Abp.Modules;
using Abp.Reflection.Extensions;
using VinaCent.Blaze.Authorization;

namespace VinaCent.Blaze
{
    [DependsOn(
        typeof(BlazeCoreModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpMailKitModule))]
    public class BlazeApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BlazeAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BlazeApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}

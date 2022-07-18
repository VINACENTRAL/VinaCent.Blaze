using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using VinaCent.Blaze.EntityFrameworkCore;
using VinaCent.Blaze.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace VinaCent.Blaze.Web.Tests
{
    [DependsOn(
        typeof(BlazeWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class BlazeWebTestModule : AbpModule
    {
        public BlazeWebTestModule(BlazeEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BlazeWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(BlazeWebMvcModule).Assembly);
        }
    }
}
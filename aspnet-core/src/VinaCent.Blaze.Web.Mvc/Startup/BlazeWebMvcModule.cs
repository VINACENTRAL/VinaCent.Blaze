using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.Web.Areas.AdminCP.Common;
using VinaCent.Blaze.Web.Areas.Client.Common;
using VinaCent.Blaze.Web.Common;
using VinaCent.Blaze.Web.Areas.BusinessCP.Common;

namespace VinaCent.Blaze.Web.Startup
{
    [DependsOn(typeof(BlazeWebCoreModule))]
    public class BlazeWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BlazeWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<AdminCpNavigationProvider>();
            Configuration.Navigation.Providers.Add<BusinessCpNavigationProvider>();
            Configuration.Navigation.Providers.Add<ClientNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BlazeWebMvcModule).GetAssembly());
        }
    }
}

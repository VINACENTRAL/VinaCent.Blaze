using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using VinaCent.Blaze.Configuration;

namespace VinaCent.Blaze.Web.Host.Startup
{
    [DependsOn(
       typeof(BlazeWebCoreModule))]
    public class BlazeWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BlazeWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BlazeWebHostModule).GetAssembly());
        }
    }
}

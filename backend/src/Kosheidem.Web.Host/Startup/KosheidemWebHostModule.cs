using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Kosheidem.Configuration;
using Kosheidem.Weeks;

namespace Kosheidem.Web.Host.Startup
{
    [DependsOn(
        typeof(KosheidemWebCoreModule))]
    public class KosheidemWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public KosheidemWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KosheidemWebHostModule).GetAssembly());
        }
    }
}
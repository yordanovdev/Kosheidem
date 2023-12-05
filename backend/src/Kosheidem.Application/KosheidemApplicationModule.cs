using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Kosheidem.Authorization;

namespace Kosheidem
{
    [DependsOn(
        typeof(KosheidemCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class KosheidemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<KosheidemAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(KosheidemApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}

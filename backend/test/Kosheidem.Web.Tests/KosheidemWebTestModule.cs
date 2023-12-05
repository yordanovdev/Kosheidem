using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Kosheidem.EntityFrameworkCore;
using Kosheidem.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Kosheidem.Web.Tests
{
    [DependsOn(
        typeof(KosheidemWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class KosheidemWebTestModule : AbpModule
    {
        public KosheidemWebTestModule(KosheidemEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KosheidemWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(KosheidemWebMvcModule).Assembly);
        }
    }
}
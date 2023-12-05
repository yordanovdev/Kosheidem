using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Kosheidem.Controllers
{
    public abstract class KosheidemControllerBase: AbpController
    {
        protected KosheidemControllerBase()
        {
            LocalizationSourceName = KosheidemConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

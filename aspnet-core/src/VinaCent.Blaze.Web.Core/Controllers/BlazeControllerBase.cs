using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace VinaCent.Blaze.Controllers
{
    public abstract class BlazeControllerBase: AbpController
    {
        protected BlazeControllerBase()
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

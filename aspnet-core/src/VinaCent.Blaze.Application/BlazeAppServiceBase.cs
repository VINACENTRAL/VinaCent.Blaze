using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.MultiTenancy;
using Abp.UI;

namespace VinaCent.Blaze
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class BlazeAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected BlazeAppServiceBase()
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new UserFriendlyException(L(LKConstants.PleaseLogInBeforeDoThisAction));
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

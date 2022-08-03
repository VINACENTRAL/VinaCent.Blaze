using Abp.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using VinaCent.Blaze.Authorization.CurrentUser;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Web.Common;
using VinaCent.Blaze.Web.Views.Profile.Components.ProfilePassword;
using VinaCent.Blaze.Web.Views.Profile.Components.ProfilePersonalInfo;
using VinaCent.Blaze.Web.Views.Profile.Components.ProfilePrivacyPolicy;

namespace VinaCent.Blaze.Web.Contributors.ProfileManagement
{
    public class ProfileManagementPageContributor : IProfileManagementPageContributor
    {
        public async Task ConfigureAsync(ProfileManagementPageCreationContext context)
        {
            var _localizationContext = context.ServiceProvider.GetRequiredService<ILocalizationContext>();

            context.Groups.Add(
                new ProfileManagementPageGroup(
                    CommonPageNames.PersonalInfo,
                    L(LKConstants.PersonalInfo).Localize(_localizationContext),
                    typeof(ProfilePersonalInfoViewComponent)
                )
            );

            if (await IsPasswordChangeEnabled(context))
            {
                context.Groups.Add(
                    new ProfileManagementPageGroup(
                        CommonPageNames.Password,
                        L(LKConstants.UpdatePassword).Localize(_localizationContext),
                        typeof(ProfilePasswordViewComponent)
                    )
                );
            }

            context.Groups.Add(
                new ProfileManagementPageGroup(
                    CommonPageNames.PrivacyPolicy,
                    L(LKConstants.PrivacyPolicy).Localize(_localizationContext),
                    typeof(ProfilePrivacyPolicyViewComponent)
                )
            );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BlazeConsts.LocalizationSourceName);
        }

        protected virtual async Task<bool> IsPasswordChangeEnabled(ProfileManagementPageCreationContext context)
        {
            var userManager = context.ServiceProvider.GetRequiredService<UserManager>();
            var currentUser = context.ServiceProvider.GetRequiredService<IAbpSession>(); 

            var user = await userManager.GetUserByIdAsync(currentUser.GetUserId());

            return user.AuthenticationSource.IsNullOrWhiteSpace();
        }
    }
}

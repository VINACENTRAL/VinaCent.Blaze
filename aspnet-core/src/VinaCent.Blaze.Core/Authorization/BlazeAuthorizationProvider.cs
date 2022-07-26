using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace VinaCent.Blaze.Authorization
{
    public class BlazeAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_FileManagement, L("FileManagement"));
            context.CreatePermission(PermissionNames.Pages_Languages, L("LanguageManagement"));
            context.CreatePermission(PermissionNames.Pages_LanguageTexts, L("LanguageTextManagement"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BlazeConsts.LocalizationSourceName);
        }
    }
}

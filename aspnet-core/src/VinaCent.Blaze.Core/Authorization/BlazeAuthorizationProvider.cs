using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace VinaCent.Blaze.Authorization
{
    public class BlazeAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L(LKConstants.Users));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L(LKConstants.UsersActivation));
            context.CreatePermission(PermissionNames.Pages_Roles, L(LKConstants.Roles));
            context.CreatePermission(PermissionNames.Pages_Tenants, L(LKConstants.Tenants), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_FileManagement, L(LKConstants.FileManagement));
            context.CreatePermission(PermissionNames.Pages_Languages, L(LKConstants.LanguageManagement));
            context.CreatePermission(PermissionNames.Pages_LanguageTexts, L(LKConstants.LanguageTextManagement));
            context.CreatePermission(PermissionNames.Pages_AuditLogs, L(LKConstants.AuditLogs));
            // Business
            context.CreatePermission(PermissionNames.Pages_CurrencyManagement, L(LKConstants.CurrencyManagement));
            // Shop module
            context.CreatePermission(PermissionNames.Pages_Shop, L(LKConstants.Shop));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BlazeConsts.LocalizationSourceName);
        }
    }
}

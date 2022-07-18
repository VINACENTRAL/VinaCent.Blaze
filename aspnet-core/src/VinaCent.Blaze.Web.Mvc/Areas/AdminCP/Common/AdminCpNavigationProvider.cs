using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using VinaCent.Blaze.Authorization;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Common;

/// <summary>
/// This class defines menus for the application.
/// </summary>
/// Ref Icons: https://themesbrand.com/velzon/html/material/icons-materialdesign.html
public class AdminCpNavigationProvider : NavigationProvider
{
    public override void SetNavigation(INavigationProviderContext context)
    {
        var adminCpMenuDefinition = new MenuDefinition(nameof(AdminCP), L(nameof(AdminCP)));

        adminCpMenuDefinition
            .AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.Dashboard,
                    L(LKConstants.Dashboard),
                    url: "admincp/dashboard",
                    icon: "mdi mdi-speedometer",
                    requiresAuthentication: true
                )
            )
            .AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.FileManagement,
                    L(LKConstants.FileManagement),
                    url: "admincp/file-management",
                    icon: "mdi mdi-folder-multiple",
                    requiresAuthentication: true,
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_FileManagement)
                )
            )
            .AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.Tenants,
                    L(LKConstants.Tenants),
                    isEnabled: BlazeConsts.MultiTenancyEnabled,
                    isVisible: BlazeConsts.MultiTenancyEnabled,
                    url: "admincp/tenants",
                    icon: "mdi mdi-office-building",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                )
            )
            .AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.IdentityManagement,
                    L(LKConstants.IdentityManagement),
                    icon: "mdi mdi-wrench",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users, PermissionNames.Pages_Roles)
                ).AddItem(
                    new MenuItemDefinition(
                        AdminCpPageNames.Users,
                        L(LKConstants.Users),
                        url: "admincp/users",
                        icon: "mdi mdi-account-multiple",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        AdminCpPageNames.Roles,
                        L(LKConstants.Roles),
                        url: "admincp/roles",
                        icon: "mdi mdi-drama-masks",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles),
                        order: 1
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        AdminCpPageNames.SecurityLogs,
                        L(LKConstants.SecurityLogs),
                        url: "admincp/security-logs",
                        icon: "mdi mdi-security",
                        order: 2
                    )
                )
            )
            .AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.LocalizationManagement,
                    L(LKConstants.LocalizationManagement),
                    icon: "mdi mdi-web",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Languages, PermissionNames.Pages_LanguageTexts)
                ).AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.LanguageManagement,
                    L(LKConstants.LanguageManagement),
                    url: "admincp/languages",
                    icon: "mdi mdi-album",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Languages)
                    )
                ).AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.LanguageTextManagement,
                    L(LKConstants.LanguageTextManagement),
                    url: "admincp/language-texts",
                    icon: "mdi mdi-translate",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_LanguageTexts),
                    order: 1
                )
              )
            ).AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.AuditLogs,
                    L(LKConstants.AuditLogs),
                    url: "admincp/audit-logs",
                    icon: "mdi mdi-calendar-check",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_AuditLogs)
                )
            ).AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.SettingManagement,
                    L(LKConstants.SettingManagement),
                    icon: "mdi mdi-cog-outline",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Languages)
                ).AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.SettingManagement_AppMeta,
                    L(LKConstants.SettingManagement_AppMeta),
                    url: "admincp/settings/meta",
                    icon: "mdi mdi-cloud-tags",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Languages),
                    order: 1
                    )
                ).AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.SettingManagement_AppTheme,
                    L(LKConstants.SettingManagement_AppTheme),
                    url: "admincp/settings/theme",
                    icon: "mdi mdi-television-guide",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Languages),
                    order: 2
                    )
                ).AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.SettingManagement_AppSystem,
                    L(LKConstants.SettingManagement_AppSystem),
                    url: "admincp/settings/system",
                    icon: "mdi mdi-store-cog",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Languages),
                    order: 3
                    )
                ).AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.EmailConfiguration,
                    L(LKConstants.EmailConfiguration),
                    icon: "mdi mdi-cog-transfer-outline",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Languages)
                    ).AddItem(
                    new MenuItemDefinition(
                        AdminCpPageNames.EmailConfiguration_SetUpMailServer,
                        L(LKConstants.EmailConfiguration_SetUpMailServer),
                        url: "admincp/email-configuration/mail-server",
                        icon: "mdi mdi-server-network",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Languages)
                        )
                    ).AddItem(
                    new MenuItemDefinition(
                        AdminCpPageNames.EmailConfiguration_TextTemplates,
                        L(LKConstants.EmailConfiguration_TextTemplates),
                        url: "admincp/email-configuration/text-templates",
                        icon: "mdi mdi-text-box-check-outline",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Languages)
                        )
                    )
                )
            );

        context.Manager.Menus.Add(nameof(AdminCP), adminCpMenuDefinition);
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, BlazeConsts.LocalizationSourceName);
    }
}

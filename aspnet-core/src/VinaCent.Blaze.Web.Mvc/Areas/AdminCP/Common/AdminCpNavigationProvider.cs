using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using VinaCent.Blaze.Authorization;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Common;

/// <summary>
/// This class defines menus for the application.
/// </summary>
/// Ref Icons: https://pictogrammers.github.io/@mdi/font/2.0.46/
public class AdminCpNavigationProvider : NavigationProvider
{
    public override void SetNavigation(INavigationProviderContext context)
    {
        var adminCpMenuDefinition = new MenuDefinition(nameof(AdminCP), L(nameof(AdminCP)));
        adminCpMenuDefinition
            .AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.FileManagement,
                    L(LKConstants.FileManagement),
                    url: "admincp/file-management",
                    icon: "mdi mdi-folder-multiple",
                    requiresAuthentication: true
                )
            )
            .AddItem(
                new MenuItemDefinition(
                    AdminCpPageNames.Tenants,
                    L(LKConstants.Tenants),
                    url: "admincp/tenants",
                    icon: "mdi mdi-office-building",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                )
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
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                )
            ).AddItem(
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
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_LanguageTexts)
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

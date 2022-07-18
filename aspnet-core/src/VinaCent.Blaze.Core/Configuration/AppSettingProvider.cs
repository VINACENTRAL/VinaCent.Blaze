using System.Collections.Generic;
using Abp.Configuration;

namespace VinaCent.Blaze.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition>
        GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
#region App meta
                new SettingDefinition(AppSettingNames.SiteTitle,
                    "Viet Nam Central Technology",
                    scopes: SettingScopes.Application |
                    SettingScopes.Tenant,
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SiteFavicon,
                    "/vinacent/brand/favicon.ico",
                    scopes: SettingScopes.Application |
                    SettingScopes.Tenant,
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SiteName,
                    "VinaCent",
                    scopes: SettingScopes.Application |
                    SettingScopes.Tenant,
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SiteDescription,
                    "Grow your business",
                    scopes: SettingScopes.Application |
                    SettingScopes.Tenant,
                    isVisibleToClients: true),

                new SettingDefinition(AppSettingNames.SiteAuthor,
                    "VINACENT Teams",
                    scopes: SettingScopes.Application |
                    SettingScopes.Tenant,
                    isVisibleToClients: true),

                new SettingDefinition(AppSettingNames.SiteAuthorProfileUrl,
                    "https://vinacent.com/teams",
                    scopes: SettingScopes.Application |
                    SettingScopes.Tenant,
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SiteHolderImage, "/Common/images/no-image.png",
                    scopes: SettingScopes.Application | SettingScopes.Tenant,
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SiteUserAvatarHolder, "/Common/images/user.png",
                    scopes: SettingScopes.Application | SettingScopes.Tenant,
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SiteUserCoverHolder, "/Common/images/profile-bg.jpg",
                    scopes: SettingScopes.Application | SettingScopes.Tenant,
                    isVisibleToClients: true),
#endregion

#region App UI/Theme
                new SettingDefinition(AppSettingNames.UiTheme,
                    "red",
                    scopes: SettingScopes.Application |
                    SettingScopes.Tenant |
                    SettingScopes.User,
                    clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UiThemeMode,
                    "",
                    scopes: SettingScopes.Application |
                    SettingScopes.Tenant |
                    SettingScopes.User,
                    clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
#endregion

#region App File Management
                new SettingDefinition(AppSettingNames.AllowedMaxFileSizeInMB,
                    "1000", //MaxFileSize = 10 KB
                    scopes: SettingScopes.Application,
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.AllowedUploadFormats,
                    ".jpeg, .jpg, .png",
                    scopes: SettingScopes.Application,
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.NoPreviewImage, "/Common/images/no-cover.jpg",
                    scopes: SettingScopes.Application | SettingScopes.Tenant,
                    isVisibleToClients: true),
                #endregion

#region App System settings
                new SettingDefinition(AppSettingNames.AppSys_DoNotShowLogoutScreen,
                    false.ToString(),
                    scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.AppSys_IsRegisterEnabled,
                    true.ToString(),
                    scopes: SettingScopes.Application | SettingScopes.Tenant),
	#endregion

#region User settings
                new SettingDefinition(AppSettingNames.User.IsUserNameUpdateEnabled,
                    false.ToString(),
                    scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.User.IsEmailUpdateEnabled,
                    true.ToString(),
                    scopes: SettingScopes.Application | SettingScopes.Tenant),
    #endregion
            };
        }
    }
}

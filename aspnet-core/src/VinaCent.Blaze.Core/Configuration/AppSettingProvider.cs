﻿using System.Collections.Generic;
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
                new SettingDefinition(AppSettingNames.AllowedMaxFileSize,
                    "10", //MaxFileSize = 10 KB
                    scopes: SettingScopes.Application,
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.AllowedUploadFormats,
                    ".jpeg, .jpg, .png",
                    scopes: SettingScopes.Application,
                    isVisibleToClients: true)
#endregion
            };
        }
    }
}

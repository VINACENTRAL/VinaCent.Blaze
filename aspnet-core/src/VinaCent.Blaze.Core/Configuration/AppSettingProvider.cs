using System.Collections.Generic;
using Abp.Configuration;

namespace VinaCent.Blaze.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.SiteName, "VinaCent",
                    scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User,
                    isVisibleToClients: true),

                new SettingDefinition(AppSettingNames.UiTheme, "red",
                scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User,
                clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),

                new SettingDefinition(AppSettingNames.AllowedMaxFileSize, "10", //MaxFileSize = 10 KB
                    scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User,
                    isVisibleToClients: true),

                new SettingDefinition(AppSettingNames.AllowedUploadFormats, ".jpeg, .jpg, .png",
                    scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User,
                    isVisibleToClients: true)
            };
        }
    }
}

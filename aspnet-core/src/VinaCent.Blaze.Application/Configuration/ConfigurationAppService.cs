using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using VinaCent.Blaze.Configuration.Dto;

namespace VinaCent.Blaze.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BlazeAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }

        public async Task ChangeUiThemeMode(ChangeUiThemeModeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiThemeMode, input.ThemeMode);
        }
    }
}

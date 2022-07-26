using System.Threading.Tasks;
using VinaCent.Blaze.Configuration.Dto;

namespace VinaCent.Blaze.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
        Task ChangeUiThemeMode(ChangeUiThemeModeInput input);
    }
}

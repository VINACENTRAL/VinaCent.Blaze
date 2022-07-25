using Abp.AspNetCore.Mvc.ViewComponents;
using Abp.Configuration.Startup;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.Sessions;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonUserArea;

public class VelzonUserAreaViewComponent : AbpViewComponent
{
    private readonly ISessionAppService _sessionAppService;
    private readonly IMultiTenancyConfig _multiTenancyConfig;

    public VelzonUserAreaViewComponent(
        ISessionAppService sessionAppService,
        IMultiTenancyConfig multiTenancyConfig)
    {
        _sessionAppService = sessionAppService;
        _multiTenancyConfig = multiTenancyConfig;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new VelzonUserAreaViewModel
        {
            LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
            IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
        };

        return View($"~/Themes/Velzon/Components/{nameof(VelzonUserAreaViewComponent).Replace("ViewComponent", "")}/Default.cshtml", model);
    }
}


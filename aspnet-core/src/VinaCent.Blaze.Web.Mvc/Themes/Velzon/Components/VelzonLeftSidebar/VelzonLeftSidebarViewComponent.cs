using Abp.Application.Navigation;
using Abp.AspNetCore.Mvc.ViewComponents;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.Web.Themes.Velzon.ThemeOptions;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonLeftSidebar;

public class VelzonLeftSidebarViewComponent : AbpViewComponent
{
    private readonly IUserNavigationManager _userNavigationManager;
    private readonly IAbpSession _abpSession;

    public VelzonLeftSidebarViewComponent(
        IUserNavigationManager userNavigationManager,
        IAbpSession abpSession)
    {
        _userNavigationManager = userNavigationManager;
        _abpSession = abpSession;
    }

    public async Task<IViewComponentResult> InvokeAsync(string menuName)
    {
        if (VelzonConsts.CPAreas.Contains(menuName))
        {
            var model = new SidebarControlMenuViewModel
            {
                AdminCpMenu = await _userNavigationManager.GetMenuAsync(nameof(Areas.AdminCP), _abpSession.ToUserIdentifier()),
                BusinessCpMenu = await _userNavigationManager.GetMenuAsync(nameof(Areas.BusinessCP), _abpSession.ToUserIdentifier())
            };

            return View($"~/Themes/Velzon/Components/{nameof(VelzonLeftSidebarViewComponent).Replace("ViewComponent", "")}/ControlDefault.cshtml", model);
        }
        else
        {
            var model = new SideBarMenuViewModel
            {
                MainMenu = await _userNavigationManager.GetMenuAsync(menuName, _abpSession.ToUserIdentifier())
            };

            return View($"~/Themes/Velzon/Components/{nameof(VelzonLeftSidebarViewComponent).Replace("ViewComponent", "")}/Default.cshtml", model);
        }
    }
}


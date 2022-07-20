using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;

namespace VinaCent.Blaze.Web.Views.Shared.Components.VelzonAside
{
    public class VelzonAsideViewComponent : BlazeViewComponent
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;

        public VelzonAsideViewComponent(
            IAbpSession abpSession, 
            IUserNavigationManager userNavigationManager)
        {
            _abpSession = abpSession;
            _userNavigationManager = userNavigationManager;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SideBarMenuViewModel
            {
                MainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", _abpSession.ToUserIdentifier())
            };

            return View(model);
        }
    }
}

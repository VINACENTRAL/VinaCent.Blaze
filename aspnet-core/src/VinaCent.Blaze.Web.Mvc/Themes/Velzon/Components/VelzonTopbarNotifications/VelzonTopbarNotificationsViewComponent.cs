using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.ViewComponents;
using Abp.Notifications;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonTopbarNotifications;

// https://aspnetboilerplate.com/Pages/Documents/Notification-System
public class VelzonTopbarNotificationsViewComponent : AbpViewComponent
{
    private readonly IUserNotificationManager _userNotificationManager;

    public VelzonTopbarNotificationsViewComponent(IUserNotificationManager userNotificationManager)
    {
        _userNotificationManager = userNotificationManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new VelzonTopbarNotificationsViewModel
        {
            UnreadAllCount =
                await _userNotificationManager.GetUserNotificationCountAsync(AbpSession.ToUserIdentifier(),
                    UserNotificationState.Unread)
        };
        
        return View(
            $"~/Themes/Velzon/Components/{nameof(VelzonTopbarNotificationsViewComponent).Remove("ViewComponent")}/Default.cshtml",
            model);
    }
}
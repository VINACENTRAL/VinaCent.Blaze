using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using VinaCent.Blaze.Authorization;

namespace VinaCent.Blaze.Web.Areas.Client.Common;

/// <summary>
/// This class defines menus for the application.
/// </summary>
public class ClientNavigationProvider : NavigationProvider
{
    public override void SetNavigation(INavigationProviderContext context)
    {
        var clientMenuDefinition = new MenuDefinition(nameof(Client), L(nameof(Client)));
        clientMenuDefinition
            .AddItem(
                new MenuItemDefinition(
                    ClientPageNames.Home,
                    L("HomePage"),
                    url: "",
                    icon: "mdi mdi-home",
                    requiresAuthentication: false
                )
            )
            .AddItem( // Menu items below is just for demonstration!
                new MenuItemDefinition(
                    "MultiLevelMenu",
                    L("MultiLevelMenu"),
                    icon: "mdi mdi-circle"
                ));

        context.Manager.Menus.Add(nameof(Client), clientMenuDefinition);
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, BlazeConsts.LocalizationSourceName);
    }
}

using Abp.Application.Navigation;
using Abp.Localization;

namespace VinaCent.Blaze.Web.Common;

/// <summary>
/// This class defines menus for the application.
/// </summary>
public class CommonNavigationProvider : NavigationProvider
{
    public override void SetNavigation(INavigationProviderContext context)
    {
        var clientMenuDefinition = new MenuDefinition("Common", L("Common"));
        //clientMenuDefinition
        //    .AddItem(
        //        new MenuItemDefinition(
        //            ClientPageNames.HomePage,
        //            L(LKConstants.HomePage),
        //            url: "",
        //            icon: "mdi mdi-home",
        //            requiresAuthentication: false
        //        )
        //    );

        context.Manager.Menus.Add("Common", clientMenuDefinition);
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, BlazeConsts.LocalizationSourceName);
    }
}

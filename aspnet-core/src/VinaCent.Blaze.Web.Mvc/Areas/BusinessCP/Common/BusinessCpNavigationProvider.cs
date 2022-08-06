using Abp.Application.Navigation;
using Abp.Localization;

namespace VinaCent.Blaze.Web.Areas.BusinessCP.Common
{
    public class BusinessCpNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.Menus.TryGetValue(nameof(AdminCP), out var businessCpMenuDefinition);
            if (businessCpMenuDefinition == null)
            {
                businessCpMenuDefinition = new MenuDefinition(nameof(AdminCP), L(nameof(AdminCP)));
            }
            businessCpMenuDefinition
                .AddItem(
                    new MenuItemDefinition(
                        BusinessCpPageNames.CurrencyUnitManagement,
                        L(LKConstants.CurrencyUnitManagement),
                        url: "businesscp/currency-unit-management",
                        icon: "mdi mdi-speedometer",
                        requiresAuthentication: true
                    )
                );

            context.Manager.Menus.Add(nameof(BusinessCP), businessCpMenuDefinition);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BlazeConsts.LocalizationSourceName);
        }
    }
}

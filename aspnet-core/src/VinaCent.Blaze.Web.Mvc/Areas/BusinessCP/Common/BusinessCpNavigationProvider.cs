using Abp.Application.Navigation;
using Abp.Localization;

namespace VinaCent.Blaze.Web.Areas.BusinessCP.Common
{
    public class BusinessCpNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            var businessCpMenuDefinition = new MenuDefinition(nameof(BusinessCP), L(nameof(BusinessCP)));
            businessCpMenuDefinition
                .AddItem(
                    new MenuItemDefinition(
                        BusinessCpPageNames.CurrencyManagement,
                        L(LKConstants.CurrencyManagement),
                        icon: "ri-coins-line"
                        //permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users, PermissionNames.Pages_Roles)
                    ).AddItem(
                        new MenuItemDefinition(
                            BusinessCpPageNames.CurrencyUnitManagement,
                            L(LKConstants.CurrencyUnitManagement),
                            url: "businesscp/currency-units",
                            icon: "ri-copper-coin-line",
                            requiresAuthentication: true
                        )).AddItem(
                        new MenuItemDefinition(
                            BusinessCpPageNames.CurrencyExchangeRateManagement,
                            L(LKConstants.CurrencyExchangeRateManagement),
                            url: "businesscp/currency-exchange-rates",
                            icon: "ri-exchange-funds-fill",
                            requiresAuthentication: true,
                            order: 1
                        ))
                )
                .AddItem(
                    new MenuItemDefinition(
                        BusinessCpPageNames.ShopPageNames.Shop,
                        L(LKConstants.Shop),
                        icon: "ri-shopping-bag-3-fill",
                        order: 1
                        //permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users, PermissionNames.Pages_Roles)
                    ).AddItem(
                        new MenuItemDefinition(
                            BusinessCpPageNames.ShopPageNames.Shop_CategoryManagement,
                            L(LKConstants.Shop_CategoryManagement),
                            url: "businesscp/shop/categories",
                            icon: "mdi mdi-rhombus-split",
                            requiresAuthentication: true
                        )).AddItem(
                        new MenuItemDefinition(
                            BusinessCpPageNames.ShopPageNames.Shop_TagManagement,
                            L(LKConstants.Shop_TagManagement),
                            url: "businesscp/shop/tags",
                            icon: "ri-price-tag-3-fill",
                            requiresAuthentication: true,
                            order: 1
                        )).AddItem(
                        new MenuItemDefinition(
                            BusinessCpPageNames.ShopPageNames.Shop_ProductManagement,
                            L(LKConstants.Shop_ProductManagement),
                            url: "businesscp/shop/products",
                            icon: "ri-archive-fill",
                            requiresAuthentication: true,
                            order: 2
                        ))
                );

            context.Manager.Menus.Add(nameof(BusinessCP), businessCpMenuDefinition);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BlazeConsts.LocalizationSourceName);
        }
    }
}
using Abp.Configuration;
using Abp.Localization;
using System.Threading.Tasks;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.SettingManagement
{
    public class AppMetaSettingModel
    {
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SiteTitle)]
        public string SiteTitle { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SiteName)]
        public string SiteName { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SiteDescription)]
        public string SiteDescription { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SiteFavicon)]
        public string SiteFavicon { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SiteAuthor)]
        public string SiteAuthor { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.SiteAuthorProfileUrl)]
        public string SiteAuthorProfileUrl { get; set; }

        public AppMetaSettingModel()
        {

        }

        public AppMetaSettingModel(ISettingManager settingManager)
        {
            SiteTitle = settingManager.GetSettingValue(AppSettingNames.SiteTitle);
            SiteName = settingManager.GetSettingValue(AppSettingNames.SiteName);
            SiteDescription = settingManager.GetSettingValue(AppSettingNames.SiteDescription);
            SiteFavicon = settingManager.GetSettingValue(AppSettingNames.SiteFavicon);
            SiteAuthor = settingManager.GetSettingValue(AppSettingNames.SiteAuthor);
            SiteAuthorProfileUrl = settingManager.GetSettingValue(AppSettingNames.SiteAuthorProfileUrl);
        }

        public async Task Save(ISettingManager settingManager, int? tenantId)
        {
            if (tenantId != null)
            {
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.SiteTitle, SiteTitle);
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.SiteName, SiteName);
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.SiteDescription, SiteDescription);
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.SiteFavicon, SiteFavicon);
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.SiteAuthor, SiteAuthor);
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.SiteAuthorProfileUrl, SiteAuthorProfileUrl);
            }
            else
            {
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.SiteTitle, SiteTitle);
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.SiteName, SiteName);
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.SiteDescription, SiteDescription);
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.SiteFavicon, SiteFavicon);
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.SiteAuthor, SiteAuthor);
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.SiteAuthorProfileUrl, SiteAuthorProfileUrl);
            }
        }
    }
}

using Abp.Configuration;
using Abp.Localization;
using System.Threading.Tasks;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.DataAnnotations;
using static Abp.Zero.Configuration.AbpZeroSettingNames;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.SettingManagement
{
    public class AppSystemSettingModel
    {
        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(AppSys_DoNotShowLogoutScreen))]
        public bool AppSys_DoNotShowLogoutScreen { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(AppSys_IsRegisterEnabled))]
        public bool AppSys_IsRegisterEnabled { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(IsUserNameUpdateEnabled))]
        public bool IsUserNameUpdateEnabled { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(IsEmailUpdateEnabled))]
        public bool IsEmailUpdateEnabled { get; set; }

        // File management

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(AllowedMaxFileSize))]
        public long AllowedMaxFileSize { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(AllowedUploadFormats))]
        public string AllowedUploadFormats { get; set; }

        #region UserManagement

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_IsEmailConfirmationRequiredForLogin))]
        public bool UserManagement_IsEmailConfirmationRequiredForLogin { get; set; }

        // UserLockOut

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_UserLockOut_IsEnabled))]
        public bool UserManagement_UserLockOut_IsEnabled { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_UserLockOut_MaxFailedAccessAttemptsBeforeLockout))]
        public int UserManagement_UserLockOut_MaxFailedAccessAttemptsBeforeLockout { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_UserLockOut_DefaultAccountLockoutSeconds))]
        public int UserManagement_UserLockOut_DefaultAccountLockoutSeconds { get; set; }

        // TwoFactorLogin

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_TwoFactorLogin_IsEnabled))]
        public bool UserManagement_TwoFactorLogin_IsEnabled { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_TwoFactorLogin_IsEmailProviderEnabled))]
        public bool UserManagement_TwoFactorLogin_IsEmailProviderEnabled { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_TwoFactorLogin_IsSmsProviderEnabled))]
        public bool UserManagement_TwoFactorLogin_IsSmsProviderEnabled { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_TwoFactorLogin_IsRememberBrowserEnabled))]
        public bool UserManagement_TwoFactorLogin_IsRememberBrowserEnabled { get; set; }

        // PasswordComplexity

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_PasswordComplexity_RequiredLength))]
        public int UserManagement_PasswordComplexity_RequiredLength { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_PasswordComplexity_RequireNonAlphanumeric))]
        public bool UserManagement_PasswordComplexity_RequireNonAlphanumeric { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_PasswordComplexity_RequireLowercase))]
        public bool UserManagement_PasswordComplexity_RequireLowercase { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_PasswordComplexity_RequireUppercase))]
        public bool UserManagement_PasswordComplexity_RequireUppercase { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_PasswordComplexity_RequireDigit))]
        public bool UserManagement_PasswordComplexity_RequireDigit { get; set; }


        // OrganizationUnits

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, nameof(UserManagement_OrganizationUnits_MaxUserMembershipCount))]
        public int UserManagement_OrganizationUnits_MaxUserMembershipCount { get; set; }
        #endregion

        public AppSystemSettingModel()
        {

        }

        public AppSystemSettingModel(ISettingManager settingManager)
        {
            AppSys_DoNotShowLogoutScreen = settingManager.GetSettingValue<bool>(AppSettingNames.AppSys_DoNotShowLogoutScreen);
            AppSys_IsRegisterEnabled = settingManager.GetSettingValue<bool>(AppSettingNames.AppSys_IsRegisterEnabled);
            AllowedMaxFileSize = settingManager.GetSettingValue<long>(AppSettingNames.AllowedMaxFileSizeInMB);
            AllowedUploadFormats = settingManager.GetSettingValue(AppSettingNames.AllowedUploadFormats);
            IsUserNameUpdateEnabled = settingManager.GetSettingValue<bool>(AppSettingNames.User.IsUserNameUpdateEnabled);
            IsEmailUpdateEnabled = settingManager.GetSettingValue<bool>(AppSettingNames.User.IsEmailUpdateEnabled);
            UserManagement_IsEmailConfirmationRequiredForLogin = settingManager.GetSettingValue<bool>(UserManagement.IsEmailConfirmationRequiredForLogin);

            UserManagement_UserLockOut_IsEnabled = settingManager.GetSettingValue<bool>(UserManagement.UserLockOut.IsEnabled);
            UserManagement_UserLockOut_MaxFailedAccessAttemptsBeforeLockout = settingManager.GetSettingValue<int>(UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout);
            UserManagement_UserLockOut_DefaultAccountLockoutSeconds = settingManager.GetSettingValue<int>(UserManagement.UserLockOut.DefaultAccountLockoutSeconds);

            UserManagement_TwoFactorLogin_IsEnabled = settingManager.GetSettingValue<bool>(UserManagement.TwoFactorLogin.IsEnabled);
            UserManagement_TwoFactorLogin_IsEmailProviderEnabled = settingManager.GetSettingValue<bool>(UserManagement.TwoFactorLogin.IsEmailProviderEnabled);
            UserManagement_TwoFactorLogin_IsSmsProviderEnabled = settingManager.GetSettingValue<bool>(UserManagement.TwoFactorLogin.IsSmsProviderEnabled);
            UserManagement_TwoFactorLogin_IsRememberBrowserEnabled = settingManager.GetSettingValue<bool>(UserManagement.TwoFactorLogin.IsRememberBrowserEnabled);

            UserManagement_PasswordComplexity_RequiredLength = settingManager.GetSettingValue<int>(UserManagement.PasswordComplexity.RequiredLength);
            UserManagement_PasswordComplexity_RequireNonAlphanumeric = settingManager.GetSettingValue<bool>(UserManagement.PasswordComplexity.RequireNonAlphanumeric);
            UserManagement_PasswordComplexity_RequireLowercase = settingManager.GetSettingValue<bool>(UserManagement.PasswordComplexity.RequireLowercase);
            UserManagement_PasswordComplexity_RequireUppercase = settingManager.GetSettingValue<bool>(UserManagement.PasswordComplexity.RequireUppercase);
            UserManagement_PasswordComplexity_RequireDigit = settingManager.GetSettingValue<bool>(UserManagement.PasswordComplexity.RequireDigit);

            UserManagement_OrganizationUnits_MaxUserMembershipCount = settingManager.GetSettingValue<int>(OrganizationUnits.MaxUserMembershipCount);
        }

        public async Task Save(ISettingManager settingManager, int? tenantId)
        {
            if (tenantId != null)
            {
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.AppSys_DoNotShowLogoutScreen, AppSys_DoNotShowLogoutScreen.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.AppSys_IsRegisterEnabled, AppSys_IsRegisterEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.User.IsUserNameUpdateEnabled, IsUserNameUpdateEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.User.IsEmailUpdateEnabled, IsEmailUpdateEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.IsEmailConfirmationRequiredForLogin, UserManagement_IsEmailConfirmationRequiredForLogin.ToString().ToLower());

                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.AllowedMaxFileSizeInMB, AllowedMaxFileSize.ToString());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.AllowedUploadFormats, AllowedUploadFormats.ToString().ToLower());

                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.UserLockOut.IsEnabled, UserManagement_UserLockOut_IsEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout, UserManagement_UserLockOut_MaxFailedAccessAttemptsBeforeLockout.ToString());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.UserLockOut.DefaultAccountLockoutSeconds, UserManagement_UserLockOut_DefaultAccountLockoutSeconds.ToString());

                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.TwoFactorLogin.IsEnabled, UserManagement_TwoFactorLogin_IsEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.TwoFactorLogin.IsEmailProviderEnabled, UserManagement_TwoFactorLogin_IsEmailProviderEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.TwoFactorLogin.IsSmsProviderEnabled, UserManagement_TwoFactorLogin_IsSmsProviderEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.TwoFactorLogin.IsRememberBrowserEnabled, UserManagement_TwoFactorLogin_IsRememberBrowserEnabled.ToString().ToLower());

                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.PasswordComplexity.RequiredLength, UserManagement_PasswordComplexity_RequiredLength.ToString());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.PasswordComplexity.RequireNonAlphanumeric, UserManagement_PasswordComplexity_RequireNonAlphanumeric.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.PasswordComplexity.RequireLowercase, UserManagement_PasswordComplexity_RequireLowercase.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.PasswordComplexity.RequireUppercase, UserManagement_PasswordComplexity_RequireUppercase.ToString().ToLower());
                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, UserManagement.PasswordComplexity.RequireDigit, UserManagement_PasswordComplexity_RequireDigit.ToString());

                await settingManager.ChangeSettingForTenantAsync(tenantId.Value, OrganizationUnits.MaxUserMembershipCount, UserManagement_OrganizationUnits_MaxUserMembershipCount.ToString());
            }
            else
            {
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.AppSys_DoNotShowLogoutScreen, AppSys_DoNotShowLogoutScreen.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.AppSys_IsRegisterEnabled, AppSys_IsRegisterEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.User.IsUserNameUpdateEnabled, IsUserNameUpdateEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.User.IsEmailUpdateEnabled, IsEmailUpdateEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.IsEmailConfirmationRequiredForLogin, UserManagement_IsEmailConfirmationRequiredForLogin.ToString().ToLower());

                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.AllowedMaxFileSizeInMB, AllowedMaxFileSize.ToString());
                await settingManager.ChangeSettingForApplicationAsync(AppSettingNames.AllowedUploadFormats, AllowedUploadFormats.ToString().ToLower());

                await settingManager.ChangeSettingForApplicationAsync(UserManagement.UserLockOut.IsEnabled, UserManagement_UserLockOut_IsEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout, UserManagement_UserLockOut_MaxFailedAccessAttemptsBeforeLockout.ToString());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.UserLockOut.DefaultAccountLockoutSeconds, UserManagement_UserLockOut_DefaultAccountLockoutSeconds.ToString());

                await settingManager.ChangeSettingForApplicationAsync(UserManagement.TwoFactorLogin.IsEnabled, UserManagement_TwoFactorLogin_IsEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.TwoFactorLogin.IsEmailProviderEnabled, UserManagement_TwoFactorLogin_IsEmailProviderEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.TwoFactorLogin.IsSmsProviderEnabled, UserManagement_TwoFactorLogin_IsSmsProviderEnabled.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.TwoFactorLogin.IsRememberBrowserEnabled, UserManagement_TwoFactorLogin_IsRememberBrowserEnabled.ToString().ToLower());

                await settingManager.ChangeSettingForApplicationAsync(UserManagement.PasswordComplexity.RequiredLength, UserManagement_PasswordComplexity_RequiredLength.ToString());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.PasswordComplexity.RequireNonAlphanumeric, UserManagement_PasswordComplexity_RequireNonAlphanumeric.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.PasswordComplexity.RequireLowercase, UserManagement_PasswordComplexity_RequireLowercase.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.PasswordComplexity.RequireUppercase, UserManagement_PasswordComplexity_RequireUppercase.ToString().ToLower());
                await settingManager.ChangeSettingForApplicationAsync(UserManagement.PasswordComplexity.RequireDigit, UserManagement_PasswordComplexity_RequireDigit.ToString());

                await settingManager.ChangeSettingForApplicationAsync(OrganizationUnits.MaxUserMembershipCount, UserManagement_OrganizationUnits_MaxUserMembershipCount.ToString());
            }
        }
    }
}

namespace VinaCent.Blaze.Configuration
{
    public static class AppSettingNames
    {
        private const string Prefix = "App";

        #region App meta
        public const string SiteTitle = Prefix + "." + nameof(SiteTitle);
        public const string SiteName = Prefix + "." + nameof(SiteName);
        public const string SiteFavicon = Prefix + "." + nameof(SiteFavicon);
        public const string SiteDescription = Prefix + "." + nameof(SiteDescription);
        public const string SiteAuthor = Prefix + "." + nameof(SiteAuthor);
        public const string SiteAuthorProfileUrl = Prefix + "." + nameof(SiteAuthorProfileUrl);
        public const string SiteHolderImage = Prefix + "." + nameof(SiteHolderImage);
        public const string SiteUserAvatarHolder = Prefix + "." + nameof(SiteUserAvatarHolder);
        public const string SiteUserCoverHolder = Prefix + "." + nameof(SiteUserCoverHolder);   
        #endregion

        #region App UI/Theme
        public const string UiTheme = Prefix + "." + nameof(UiTheme);
        public const string UiThemeMode = Prefix + "." + nameof(UiThemeMode);
        #endregion

        #region App File Management
        public const string AllowedMaxFileSizeInMB = Prefix + "." + nameof(AllowedMaxFileSizeInMB);
        public const string AllowedUploadFormats = Prefix + "." + nameof(AllowedUploadFormats);
        public const string NoPreviewImage = Prefix + "." + nameof(NoPreviewImage);
        #endregion

        #region App System settings
        public const string AppSys_DoNotShowLogoutScreen = "AppSys.DoNotShowLogoutScreen";
        public const string AppSys_IsRegisterEnabled = "AppSys.IsRegisterEnabled";
        #endregion

        #region User
        public class User
        {
            private const string UserPrefix = Prefix + "." + nameof(User);
            public const string IsUserNameUpdateEnabled = UserPrefix + "." + nameof(IsUserNameUpdateEnabled);
            public const string IsEmailUpdateEnabled = UserPrefix + "." + nameof(IsEmailUpdateEnabled);
        }
        #endregion
    }
}

namespace VinaCent.Blaze.Configuration
{
    public static class AppSettingNames
    {
        private const string Prefix = "App";

        #region App meta
        public const string SiteTitle = "App.SiteTitle";
        public const string SiteName = "App.SiteName";
        public const string SiteFavicon = "App.Favicon";
        public const string SiteDescription = "App.SiteDescription";
        public const string SiteAuthor = "App.Author";
        public const string SiteAuthorProfileUrl = "App.AuthorProfileUrl";
        #endregion

        #region App UI/Theme
        public const string UiTheme = "App.UiTheme";
        public const string UiThemeMode = "App.UiThemeMode";
        #endregion

        #region App File Management
        public const string AllowedMaxFileSize = Prefix + "." + nameof(AllowedMaxFileSize);
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
            private const string UserPrefix = Prefix + ".User";

            public const string IsUserNameUpdateEnabled = UserPrefix + ".IsUserNameUpdateEnabled";
            public const string IsEmailUpdateEnabled = UserPrefix + ".IsEmailUpdateEnabled";
        }
        #endregion
    }
}

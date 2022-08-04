namespace VinaCent.Blaze.Configuration
{
    public static class AppSettingNames
    {
        private const string Prefix = "App";

        #region App meta
        public const string SiteTitle = "App.SiteTitle";
        public const string SiteName = "App.SiteName";
        public const string SiteDescription = "App.SiteDescription";
        public const string SiteAuthor = "App.Author";
        public const string SiteAuthorProfileUrl = "App.AuthorProfileUrl";
        #endregion

        #region App UI/Theme
        public const string UiTheme = "App.UiTheme";
        public const string UiThemeMode = "App.UiThemeMode";
        #endregion

        #region App File Management
        public const string AllowedMaxFileSize = "App.AllowedMaxFileSize";
        public const string AllowedUploadFormats = "App.AllowedUploadFormats";
        #endregion

        #region App System settings
        public const string AppSys_DoNotShowLogoutScreen = "AppSys.DoNotShowLogoutScreen";
        #endregion

        #region User
        public static class User
        {
            private const string UserPrefix = Prefix + ".User";

            public const string IsUserNameUpdateEnabled = UserPrefix + ".IsUserNameUpdateEnabled";
            public const string IsEmailUpdateEnabled = UserPrefix + ".IsEmailUpdateEnabled";
        }
        #endregion
    }
}

namespace VinaCent.Blaze.Web.Themes.Velzon.ThemeOptions
{
    public static class VelzonConsts
    {
        public static string[] CPAreas = new[] { nameof(Areas.AdminCP), nameof(Areas.BusinessCP) };

        public static class LayoutDirections
        {
            public const string Vertical = "vertical";
            public const string Horizontial = "horizontal";
        }

        public static class UiMode
        {
            public const string Light = "light";
            public const string Dark = "dark";
        }
    }
}

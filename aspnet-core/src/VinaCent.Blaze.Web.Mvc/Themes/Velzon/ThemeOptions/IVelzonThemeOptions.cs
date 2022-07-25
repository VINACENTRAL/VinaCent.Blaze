using Abp.Dependency;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonBreadcrumb;

namespace VinaCent.Blaze.Web.Themes.Velzon.ThemeOptions
{
    public interface IVelzonThemeOptions : ITransientDependency
    {
        public bool IsDisplayBreadcrumb { get; set; }

        public string FooterClasses { get; set; }

        public string UiMode { get; set; }

        public string LayoutDirection { get; set; }

        public VelzonBreadcrumbOptions Breadcrumb { get; set; }

        public IVelzonThemeOptions HideBreadcrumb();

        public void Load(RazorPage page);

        public IVelzonThemeOptions Backable(string backUri);
        public IVelzonThemeOptions LayoutVertial();
        public IVelzonThemeOptions LayoutHorizontal();

        public IVelzonThemeOptions AddBreadcrumb(string title, string url = "");

        public IVelzonThemeOptions AddBreadcrumb(LocalizedHtmlString displayName, string hyperLink = "");

        public void Commit<TPage>(TPage page) where TPage : RazorPageBase;
    }
}

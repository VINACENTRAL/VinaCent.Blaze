using Abp.Dependency;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonBreadcrumb;

namespace VinaCent.Blaze.Web.Themes.Velzon.ThemeOptions
{
    public class VelzonThemeOptions : IVelzonThemeOptions, ITransientDependency
    {
        public bool IsDisplayBreadcrumb { get; set; } = true;
        public string FooterClasses { get; set; }
        public string UiMode { get; set; }
        public VelzonBreadcrumbOptions Breadcrumb { get; set; }

        //private readonly IRepository<AppDictionary, Guid> AppDictionaryRepository;

        public VelzonThemeOptions()
        {
            IsDisplayBreadcrumb = true;
            FooterClasses = "";
            Breadcrumb = new VelzonBreadcrumbOptions();
            //AppDictionaryRepository = appDictionaryRepository;
        }

        public IVelzonThemeOptions HideBreadcrumb()
        {
            IsDisplayBreadcrumb = false;
            return this;
        }

        public void Load(RazorPage page)
        {
            if (page.ViewBag.VelzonThemeOptions != null)
            {
                var options = ((VelzonThemeOptions)page.ViewBag.VelzonThemeOptions);
                if (options != null)
                {
                    IsDisplayBreadcrumb = options.IsDisplayBreadcrumb;
                    FooterClasses = options.FooterClasses;
                    Breadcrumb = options.Breadcrumb;
                }
            }

            //LoadUiMode();
        }

        //private void LoadUiMode()
        //{
        //    if (CurrentUser.IsAuthenticated)
        //    {
        //        var uiMode = AppDictionaryRepository.FirstOrDefaultAsync(x =>
        //            x.Owner == CurrentUser.Id && x.Key == AppMacroSettings.UiMode)
        //                .GetAwaiter().GetResult();
        //        UiMode = uiMode?.Value?.ToLower();
        //    }
        //    UiMode ??= UiModes.Light.ToLower();
        //}

        public void Commit<TPage>(TPage page) where TPage : RazorPageBase
        {
            page.ViewBag.VelzonThemeOptions = this;
        }

        public IVelzonThemeOptions AddBreadcrumb(string title, string url = "")
        {
            if (Breadcrumb == null)
            {
                Breadcrumb = VelzonBreadcrumbOptions.Options;
            }
            Breadcrumb.AddBreadcrumb(title, url);
            return this;
        }

        public IVelzonThemeOptions Backable(string backUri)
        {
            if (Breadcrumb == null)
            {
                Breadcrumb = VelzonBreadcrumbOptions.Options;
            }
            Breadcrumb.BackUri = backUri;
            return this;
        }

        public IVelzonThemeOptions AddBreadcrumb(LocalizedHtmlString displayName, string hyperLink = "")
        {
            return AddBreadcrumb(displayName.Value, hyperLink);
        }

    }
}

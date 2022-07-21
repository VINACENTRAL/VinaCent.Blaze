using Abp.Extensions;
using Microsoft.AspNetCore.Mvc.Localization;
using System;
using System.Collections.Generic;

namespace VinaCent.Blaze.Web.Themes.Velzon.Components.VelzonBreadcrumb;

public class VelzonBreadcrumbOptions
{
    public string OverrideBreadcumbTitle { get; set; }

    public string BackUri { get; set; }

    public Dictionary<string, string> Breadcrumbs { get; set; }

    public static VelzonBreadcrumbOptions Options => new();

    public VelzonBreadcrumbOptions(string overrideBreadcumbTitle)
    {
        OverrideBreadcumbTitle = overrideBreadcumbTitle;
    }

    public VelzonBreadcrumbOptions()
    {
    }

    public VelzonBreadcrumbOptions AddBreadcrumb(string displayName, string hyperLink = "javascript:void(0);")
    {
        if (hyperLink.IsNullOrWhiteSpace())
        {
            hyperLink = "javascript:void(0);";
        }
        Breadcrumbs ??= new Dictionary<string, string>();
        Breadcrumbs.Add(displayName.Trim(), hyperLink);
        return this;
    }

    public VelzonBreadcrumbOptions AddBreadcrumb(LocalizedHtmlString displayName, string hyperLink = "javascript:void(0);")
    {
        return AddBreadcrumb(displayName.Value, hyperLink);
    }
}

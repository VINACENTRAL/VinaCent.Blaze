using Abp.Configuration;
using Abp.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using VinaCent.Blaze.Configuration;

namespace VinaCent.Blaze.Web.TagHelpers;

[HtmlTargetElement("img")]
public class ImageTagHelper : TagHelper
{
    private readonly ISettingManager _settingManager;

    public ImageTagHelper(ISettingManager settingManager)
    {
        _settingManager = settingManager;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var imageHolder = _settingManager.GetSettingValue(AppSettingNames.SiteHolderImage);
        output.TagMode = TagMode.SelfClosing;
        
        if (output.Attributes.FirstOrDefault(attr => attr.Name == "avatar") != null)
        {
            imageHolder = _settingManager.GetSettingValue(AppSettingNames.SiteUserAvatarHolder);
        }
        
        var onerror = output.Attributes.FirstOrDefault(attr => attr.Name == "onerror")?.Value?.ToString();
        
        if (onerror is {Length: > 0})
        {
            // Prevent XSS
            if (onerror.Contains("http://", StringComparison.OrdinalIgnoreCase) || onerror.Contains("https://", StringComparison.OrdinalIgnoreCase))
            {
                output.Attributes.SetAttribute("onerror", $"this.src='{imageHolder}'");
            }
        }
        else
        {
            output.Attributes.Add("onerror", $"this.src='{imageHolder}'");
        }

        var src = output.Attributes.FirstOrDefault(attr => attr.Name == "src")?.Value?.ToString() ?? "";
        if (src.IsNullOrEmpty())
        {
            output.Attributes.SetAttribute("src", imageHolder);
        }
    }
}
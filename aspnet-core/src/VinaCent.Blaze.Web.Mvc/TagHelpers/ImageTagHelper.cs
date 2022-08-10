using Abp.Configuration;
using Abp.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.Helpers;

namespace VinaCent.Blaze.Web.TagHelpers
{
    [HtmlTargetElement("img")]
    public class ImageTagHelper : TagHelper
    {
        private readonly ISettingManager _settingManager;
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ImageTagHelper(ISettingManager settingManager,
            IHostEnvironment environment,
            IConfiguration configuration)
        {
            _settingManager = settingManager;
            _environment = environment;
            _configuration = configuration;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var imageHolder = _settingManager.GetSettingValue(AppSettingNames.SiteHolderImage);
            output.TagMode = TagMode.SelfClosing;
            
            if (output.Attributes.FirstOrDefault(attr => attr.Name == "avatar") != null)
            {
                imageHolder = _settingManager.GetSettingValue(AppSettingNames.SiteUserAvatarHolder);
            }
            
            if (_environment.IsDevelopment())
            {
                var fileServerUri = _configuration.GetValue<string>("FileServer");

                if (fileServerUri.IsNullOrEmpty() ||
                    (!fileServerUri.StartsWith("https://") && !fileServerUri.StartsWith("http://")) ||
                    new Uri(fileServerUri).IsLoopback) return;

                var src = output.Attributes.FirstOrDefault(x => x.Name == "src")?.Value?.ToString();
                if (src is {Length: > 0} && !src.StartsWith("http://") && !src.StartsWith("https://"))
                {
                    src = StringHelper.TrueCombine(fileServerUri.TrimEnd('/'), src.EnsureStartsWith('/'));
                }

                output.Attributes.SetAttribute("src", src);
            }

            var onerror = output.Attributes.FirstOrDefault(attr => attr.Name == "onerror")?.Value?.ToString();
            if (onerror is {Length: > 0})
            {
                // Prevent XSS
                if (onerror.StartsWith("http://") || onerror.StartsWith("https://"))
                {
                    output.Attributes.SetAttribute("onerror", $"this.src='{imageHolder}'");
                }
            }
            else
            {
                output.Attributes.Add("onerror", $"this.src='{imageHolder}'");
            }
        }
    }
}
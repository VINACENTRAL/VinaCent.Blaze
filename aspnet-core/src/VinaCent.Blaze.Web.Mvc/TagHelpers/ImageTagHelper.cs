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
            var host = ""; // For current
            var imageHolder = _settingManager.GetSettingValue(AppSettingNames.SiteHolderImage);
            output.TagMode = TagMode.SelfClosing;
            if (_environment.IsDevelopment())
            {
                var _host = _configuration.GetValue<string>("HostUrls");
                if (!_host.IsNullOrWhiteSpace() && _host.Contains(";"))
                {
                    var hostList = _host.Split(";");
                    _host = hostList.FirstOrDefault(x => new Uri(x).Host != "localhost") ??
                            hostList.FirstOrDefault() ?? "";
                }

                if (new Uri(_host).Host != "localhost")
                {
                    host = _host;
                }

                imageHolder = StringHelper.TrueCombine(host, imageHolder);
                var src = output.Attributes.FirstOrDefault(x => x.Name == "src")?.Value?.ToString();
                if (src is { Length: > 0 } &&
                    !src.StartsWith("http://") &&
                    !src.StartsWith("https://"))
                {
                    src = StringHelper.TrueCombine(host, src);
                }

                output.Attributes.SetAttribute("src", src);
            }

            output.Attributes.Add("onerror", $"this.src='{imageHolder}'");
        }
    }
}

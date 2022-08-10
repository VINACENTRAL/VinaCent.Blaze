using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Timing;
using Microsoft.Extensions.Hosting;

namespace VinaCent.Blaze.Web.Resources
{
    public class WebResourceManager : IWebResourceManager
    {
        private readonly IWebHostEnvironment _environment;
        private readonly List<string> _scriptUrls;
        private readonly List<string> _stylesUrls;

        public WebResourceManager(IWebHostEnvironment environment)
        {
            _environment = environment;
            _scriptUrls = new List<string>();
            _stylesUrls = new List<string>();
        }

        public void AddScript(string url, bool addMinifiedOnProd = true)
        {
            _scriptUrls.AddIfNotContains(NormalizeUrl(url, "js", addMinifiedOnProd));
        }

        public IReadOnlyList<string> GetScripts()
        {
            return _scriptUrls.ToImmutableList();
        }

        public HelperResult RenderScripts()
        {
            return new HelperResult(async writer =>
            {
                foreach (var scriptUrl in _scriptUrls)
                {
                    await writer.WriteAsync($"<script src=\"{scriptUrl}?v=" + Clock.Now.Ticks + "\"></script>");
                }
            });
        }

        public void AddStyle(string url, bool addMinifiedOnProd = true)
        {
            _stylesUrls.AddIfNotContains(NormalizeUrl(url, "css", addMinifiedOnProd));
        }

        public IReadOnlyList<string> GetStyles()
        {
            return _stylesUrls.ToImmutableList();
        }

        public HelperResult RenderStyles()
        {
            return new HelperResult(async writer =>
            {
                foreach (var scriptUrl in _stylesUrls)
                {
                    await writer.WriteAsync($"<link href=\"{scriptUrl}?v={Clock.Now.Ticks}\" rel=\"stylesheet\">");
                }
            });
        }

        private string NormalizeUrl(string url, string ext, bool addMinifiedOnProd)
        {
            if (_environment.IsDevelopment())
            {
                return url;
            }

            if (url.EndsWith(".min." + ext) || !addMinifiedOnProd)
            {
                return url;
            }

            return url.Left(url.Length - ext.Length) + "min." + ext;
        }
    }
}

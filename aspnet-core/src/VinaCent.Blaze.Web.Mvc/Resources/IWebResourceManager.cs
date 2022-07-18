using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace VinaCent.Blaze.Web.Resources
{
    public interface IWebResourceManager
    {
        void AddScript(string url, bool addMinifiedOnProd = true);

        IReadOnlyList<string> GetScripts();

        HelperResult RenderScripts();
        
        void AddStyle(string url, bool addMinifiedOnProd = true);
        
        IReadOnlyList<string> GetStyles();

        HelperResult RenderStyles();
    }
}

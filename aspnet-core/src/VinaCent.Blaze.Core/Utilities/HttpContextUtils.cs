using Microsoft.AspNetCore.Http;

namespace VinaCent.Blaze.Utilities
{
    public static class HttpContextUtils
    {
        public static string GetCurrentUri(this HttpRequest currentRequest)
        {
            return currentRequest.Scheme + "://" + currentRequest.Host + currentRequest.Path;
        }
    }
}

using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace VinaCent.Blaze.Web.Views
{
    public abstract class BlazeRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected BlazeRazorPage()
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;
        }
    }
}

using Abp.AspNetCore.Mvc.ViewComponents;

namespace VinaCent.Blaze.Web.Views
{
    public abstract class BlazeViewComponent : AbpViewComponent
    {
        protected BlazeViewComponent()
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;
        }
    }
}

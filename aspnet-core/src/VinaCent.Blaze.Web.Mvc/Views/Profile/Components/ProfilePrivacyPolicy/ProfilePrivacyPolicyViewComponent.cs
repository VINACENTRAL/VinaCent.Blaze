using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VinaCent.Blaze.Web.Views.Profile.Components.ProfilePrivacyPolicy
{
    public class ProfilePrivacyPolicyViewComponent : BlazeViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Views/Profile/Components/ProfilePrivacyPolicy/Default.cshtml");
        }
    }
}

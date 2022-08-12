using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Controllers;

[Route("chat")]
[AbpAuthorize]
public class ChatController : BlazeControllerBase
{
    // GET
    public IActionResult Index()
    {
        AddSuccessNotify("Ahihi 123");
        return View();
    }
}
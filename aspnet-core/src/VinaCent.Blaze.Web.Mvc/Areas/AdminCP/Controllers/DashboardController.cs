using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers;

[AbpMvcAuthorize]
[Area("AdminCP")]
[Route("admincp/dashboard")]
public class DashboardController : BlazeControllerBase
{
    [HttpGet("/admincp")]
    public IActionResult AdminCPRedirector()
    {
        return RedirectToAction(nameof(Index));
    }

    // GET
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
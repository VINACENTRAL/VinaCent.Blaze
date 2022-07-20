using Microsoft.AspNetCore.Mvc;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers;

public class Dashboard : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.Client.Controller;

[Area("Client")]
[Route("/shop/check-out")]
public class ShopCheckoutController : BlazeControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
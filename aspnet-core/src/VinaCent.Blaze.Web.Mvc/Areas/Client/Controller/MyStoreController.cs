using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.Client.Controller;

[Area("Client")]
[Route("/shop/my-store")]
public class MyStoreController: BlazeControllerBase
{
    [HttpGet("add-product")]
    public ActionResult AddProduct()
    {
        return View();
    }
}
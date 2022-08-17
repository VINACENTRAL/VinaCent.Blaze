using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers;

[AbpMvcAuthorize]
[Area("AdminCP")]
[Route("admincp/dashboard")]
public class DashboardController : BlazeControllerBase
{
    private readonly UserManager _userManager;

    public DashboardController(UserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("/admincp")]
    public async Task<IActionResult> AdminCPRedirector()
    {
        if (!await ValidateAccess())
        {
            return Forbid();
        }

        return RedirectToAction(nameof(Index));
    }

    // GET
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        if (!await ValidateAccess())
        {
            return Forbid();
        }

        return View();
    }

    private async Task<bool> ValidateAccess()
    {
        if (AbpSession.UserId.HasValue)
        {
            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles?.Count > 0)
                    return true;
            }
        }

        return false;
    }
}
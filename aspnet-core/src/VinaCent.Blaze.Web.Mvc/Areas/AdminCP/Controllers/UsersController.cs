using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;
using VinaCent.Blaze.Users;
using VinaCent.Blaze.Web.Areas.AdminCP.Models.Users;

namespace VinaCent.Blaze.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    [Area("AdminCP")]
    [Route("admincp/users")]
    public class UsersController : BlazeControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet("")]
        public async Task<ActionResult> Index()
        {
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new UserListViewModel
            {
                Roles = roles
            };
            return View(model);
        }

        [HttpPost("edit-modal")]
        public async Task<ActionResult> EditModal(long userId)
        {
            var user = await _userAppService.GetAsync(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles
            };
            return PartialView("_EditModal", model);
        }

        [HttpGet("change-password")]
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}

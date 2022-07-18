using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.Languages;
using VinaCent.Blaze.AppCore.Languages.Dto;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Languages)]
    [Area("AdminCP")]
    [Route("admincp/languages")]
    public class LanguagesController : BlazeControllerBase
    {
        private readonly ILanguageManagementAppService _languageManagementAppService;

        public LanguagesController(ILanguageManagementAppService languageManagementAppService)
        {
            _languageManagementAppService = languageManagementAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("edit-modal")]
        public async Task<ActionResult> EditModal(int id)
        {
            var languageDto = await _languageManagementAppService.GetAsync(new EntityDto<int>(id));
            var model = ObjectMapper.Map<UpdateLanguageDto>(languageDto);
            return PartialView("_EditModal", model);
        }
    }
}

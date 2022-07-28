using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.LanguageTexts;
using VinaCent.Blaze.AppCore.LanguageTexts.Dto;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_LanguageTexts)]
    [Area("AdminCP")]
    [Route("admincp/language-texts")]
    public class LanguageTextsController : BlazeControllerBase
    {
        private readonly ILanguageTextManagementAppService _languageTextManagementAppService;

        public LanguageTextsController(ILanguageTextManagementAppService languageTextManagementAppService)
        {
            _languageTextManagementAppService = languageTextManagementAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("edit-modal")]
        public async Task<ActionResult> EditModal(int languageTextId)
        {
            var languageTextDto = await _languageTextManagementAppService.GetAsync(new EntityDto<long>(languageTextId));
            var model = ObjectMapper.Map<UpdateLanguageTextDto>(languageTextDto);
            return PartialView("_EditModal", model);
        }
    }
}

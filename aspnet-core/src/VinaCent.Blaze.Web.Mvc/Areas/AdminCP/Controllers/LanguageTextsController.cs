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
        private const string IndexView = "~/Areas/AdminCP/Views/LanguageTexts/Index.cshtml";
        private const string EditView = "~/Areas/AdminCP/Views/LanguageTexts/_EditModal.cshtml";
        private const string GroupLanguageView = "~/Areas/AdminCP/Views/LanguageTexts/_GroupLanguageModal.cshtml";

        private readonly ILanguageTextManagementAppService _languageTextManagementAppService;

        public LanguageTextsController(ILanguageTextManagementAppService languageTextManagementAppService)
        {
            _languageTextManagementAppService = languageTextManagementAppService;
        }

        public IActionResult Index()
        {
            return View(IndexView);
        }

        [HttpPost("edit-modal")]
        public async Task<ActionResult> EditModal(long id)
        {
            var languageTextDto = await _languageTextManagementAppService.GetAsync(new EntityDto<long>(id));
            var model = ObjectMapper.Map<UpdateLanguageTextDto>(languageTextDto);
            return PartialView(EditView, model);
        }

        [HttpPost("render-group-modal")]
        public async Task<ActionResult> RenderGroupModal(long? id)
        {
            var group = await _languageTextManagementAppService.GetGroupLanguageTextAsync(id);
            return PartialView(GroupLanguageView, group);
        }
    }
}

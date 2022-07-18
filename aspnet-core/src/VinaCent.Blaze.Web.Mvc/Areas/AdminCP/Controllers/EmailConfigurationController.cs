using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.Emailer;
using VinaCent.Blaze.AppCore.TextTemplates;
using VinaCent.Blaze.AppCore.TextTemplates.Dto;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize]
    [Area("AdminCP")]
    [Route("admincp/email-configuration")]
    public class EmailConfigurationController : BlazeControllerBase
    {
        private readonly ITextTemplateAppService _textTemplateAppService;
        private readonly IEmailerAppService _emailerAppService;

        public EmailConfigurationController(
            ITextTemplateAppService textTemplateAppService,
            IEmailerAppService emailerAppService)
        {
            _textTemplateAppService = textTemplateAppService;
            _emailerAppService = emailerAppService;
        }

        public IActionResult Index() => NotFound();

        [HttpGet("mail-server")]
        public async Task<IActionResult> SetUpMailServer()
        {
            var model = await _emailerAppService.GetSetupAsync();
            return View(model);
        }

        [HttpGet("text-templates")]
        public IActionResult TextTemplates()
        {
            return View();
        }

        [HttpPost("text-templates/edit-modal")]
        public async Task<ActionResult> EditModal(Guid id)
        {
            var dto = await _textTemplateAppService.GetAsync(new EntityDto<Guid>(id));
            ViewBag.IsStatic = dto.IsStatic;
            var updateDto = ObjectMapper.Map<UpdateTextTemplateDto>(dto);
            return PartialView("~/Areas/AdminCP/Views/EmailConfiguration/TextTemplates/_EditModal.cshtml", updateDto);
        }
    }
}

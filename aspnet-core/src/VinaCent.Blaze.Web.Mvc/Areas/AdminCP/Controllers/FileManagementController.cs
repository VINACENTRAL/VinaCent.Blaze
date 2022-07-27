using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.FileUnits;
using VinaCent.Blaze.AppCore.FileUnits.Dto;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_FileManagement)]
    [Area("AdminCP")]
    [Route("admincp/file-management")]
    public class FileManagementController : BlazeControllerBase
    {
        private readonly IFileUnitAppService _fileUnitAppService;

        public FileManagementController(IFileUnitAppService fileUnitAppService)
        {
            _fileUnitAppService = fileUnitAppService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("rename-modal")]
        public async Task<ActionResult> RenameModal(Guid fileUnitId)
        {
            var fileUnitDto = await _fileUnitAppService.GetAsync(fileUnitId);
            var model = ObjectMapper.Map<FileUnitRenameDto>(fileUnitDto);
            return PartialView("_RenameFileUnitModal", model);
        }
    }
}

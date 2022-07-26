using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.AppCore.FileUnits;
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
    }
}

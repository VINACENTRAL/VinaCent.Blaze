using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.AppCore.FileUnits;
using VinaCent.Blaze.Helpers;

namespace VinaCent.Blaze.Controllers
{
    /// <inheritdoc />
    [AllowAnonymous]
    [Route(AppFileResourceHelper.ResourcePathPrefix)]
    public class ResourcesController : BlazeControllerBase
    {
        private readonly IFileUnitAppService _fileUnitAppService;

        /// <inheritdoc />
        public ResourcesController(IFileUnitAppService fileUnitAppService)
        {
            _fileUnitAppService = fileUnitAppService;
        }

        /// <summary>
        /// Download file form virtual resources
        /// </summary>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        [HttpGet("{*routeValues}")] // abc.com/resource/x/y/z/a => x/y/z/a = routeValues
        public async Task<IActionResult> Index(string routeValues)
        {
            // Get virtual file
            try
            {
                var file = await _fileUnitAppService.GetByFullName(routeValues);
                if (file == null || file.IsFolder)
                    return NotFound();

                var stream = System.IO.File.OpenRead(file.PhysicalPath);
                return File(stream, "application/octet-stream");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Abp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using VinaCent.Blaze.AppCore.FileUnits;
using VinaCent.Blaze.Helpers;

namespace VinaCent.Blaze.Controllers
{
    /// <inheritdoc />
    [AllowAnonymous]
    [Route(AppFileResourceHelper.ResourcePathPrefix)]
    public class ResourcesController : BlazeControllerBase
    {
        private readonly FileUnitManager _fileUnitManager;

        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        /// <inheritdoc />
        public ResourcesController(IHostEnvironment environment,
            IConfiguration configuration,
            FileUnitManager fileUnitManager)
        {
            _environment = environment;
            _configuration = configuration;
            _fileUnitManager = fileUnitManager;
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
                var file = await _fileUnitManager.GetByFullName(routeValues);
                if (file == null)
                {
                    if (!_environment.IsDevelopment()) return NotFound();
                    var fileServerUri = _configuration.GetValue<string>("FileServer");
                    using (var client = new HttpClient())
                    {
                        if (fileServerUri.IsNullOrEmpty() ||
                            (!fileServerUri.StartsWith("https://") && !fileServerUri.StartsWith("http://")) ||
                            new Uri(fileServerUri).IsLoopback) return NotFound();

                        var fileServerResource = StringHelper.TrueCombine(fileServerUri.TrimEnd('/'), AppFileResourceHelper.ResourcePathPrefix.EnsureStartsWith('/'), routeValues.EnsureStartsWith('/'));
                        using (var result = await client.GetAsync(fileServerResource))
                        {
                            if (result.IsSuccessStatusCode)
                            {
                                return File(await result.Content.ReadAsByteArrayAsync(), "application/octet-stream");
                            }

                        }
                    }

                    return NotFound();
                }

                if (file.IsFolder)
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
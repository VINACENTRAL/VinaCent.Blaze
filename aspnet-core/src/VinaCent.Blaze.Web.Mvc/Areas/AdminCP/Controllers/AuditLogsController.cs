using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.AuditLogs;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_AuditLogs)]
    [Area("AdminCP")]
    [Route("admincp/audit-logs")]
    public class AuditLogsController : BlazeControllerBase
    {
        private readonly IAuditLogAppService _auditLogAppService;

        public AuditLogsController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("detail-modal")]
        public async Task<ActionResult> DetailModal(long id)
        {
            var auditLogDto = await _auditLogAppService.GetAsync(id);
            return PartialView("_DetailModal", auditLogDto);
        }
    }
}

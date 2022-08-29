using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.CommonDatas;
using VinaCent.Blaze.AppCore.CommonDatas.Dto;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_CommonDataManagement)]
    [Area("AdminCP")]
    [Route("admincp/common-datas")]
    public class CommonDatasController : BlazeControllerBase
    {
        private const string IndexView = "~/Areas/AdminCP/Views/CommonDatas/Index.cshtml";
        private const string EditView = "~/Areas/AdminCP/Views/CommonDatas/_EditModal.cshtml";

        private readonly ICommonDataAppService _commonDataAppService;

        public CommonDatasController(ICommonDataAppService commonDataAppService)
        {
            _commonDataAppService = commonDataAppService;
        }

        public IActionResult Index()
        {
            return View(IndexView);
        }

        [HttpPost("edit-modal")]
        public async Task<ActionResult> EditModal(Guid id)
        {
            var commonDataDto = await _commonDataAppService.GetAsync(new EntityDto<Guid>(id));
            var model = ObjectMapper.Map<UpdateCommonDataDto>(commonDataDto);
            return PartialView(EditView, model);
        }
    }
}

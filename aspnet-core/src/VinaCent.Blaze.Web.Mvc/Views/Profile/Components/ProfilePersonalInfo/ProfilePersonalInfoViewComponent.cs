using Abp.Domain.Uow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.CommonDatas;
using VinaCent.Blaze.AppCore.CommonDatas.Dto;
using VinaCent.Blaze.Profiles;

namespace VinaCent.Blaze.Web.Views.Profile.Components.ProfilePersonalInfo
{
    public class ProfilePersonalInfoViewComponent : BlazeViewComponent
    {
        private readonly IProfileAppService _profileAppService;
        private readonly ICommonDataAppService _commonDataAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ProfilePersonalInfoViewComponent(
            IProfileAppService profileAppService,
            ICommonDataAppService commonDataAppService,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _profileAppService = profileAppService;
            _commonDataAppService = commonDataAppService;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _profileAppService.GetAsync();
            using var uow = _unitOfWorkManager.Begin();
            var countries = await _commonDataAppService.GetAllList(new PagedCommonDataResultRequestDto
            {
                Type = "COUNTRY"
            });
            await uow.CompleteAsync();
            ViewData["Countries"] = countries.Items.OrderBy(x => x.Value).Select(x => new SelectListItem($"{x.Value} ({x.Description})", x.Key));
            return View("~/Views/Profile/Components/ProfilePersonalInfo/Default.cshtml", user);
        }
    }
}
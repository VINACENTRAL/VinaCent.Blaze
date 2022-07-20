using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Users;

namespace VinaCent.Blaze.Web.Views.Shared.Themes.Velzon.Components.Vertical.HeaderUserArea;

public class HeaderUserAreaViewComponent : BlazeViewComponent
{
    private readonly IAbpSession _abpSession;
    private readonly IUserAppService _userAppService;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public HeaderUserAreaViewComponent(
        IAbpSession abpSession, 
        IUserAppService userAppService, 
        IUnitOfWorkManager unitOfWorkManager)
    {
        _abpSession = abpSession;
        _userAppService = userAppService;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        using var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress);
        var currentUser = await _userAppService.GetAsync(new EntityDto<long>() {Id = _abpSession.GetUserId()});
        await uow.CompleteAsync();
        return View("~/Views/Shared/Themes/Velzon/Components/Vertical/HeaderUserArea/Default.cshtml", currentUser);
    }
}
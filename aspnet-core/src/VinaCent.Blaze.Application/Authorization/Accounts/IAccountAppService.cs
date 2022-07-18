using System.Threading.Tasks;
using Abp.Application.Services;
using VinaCent.Blaze.Authorization.Accounts.Dto;

namespace VinaCent.Blaze.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
        Task ResetPassword(ResetPasswordInput input);
    }
}

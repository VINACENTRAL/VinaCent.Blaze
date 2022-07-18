using System.Threading.Tasks;
using Abp.Application.Services;
using VinaCent.Blaze.Sessions.Dto;

namespace VinaCent.Blaze.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

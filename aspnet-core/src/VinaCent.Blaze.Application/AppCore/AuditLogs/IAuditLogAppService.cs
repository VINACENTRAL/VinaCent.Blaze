using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.AuditLogs.Dto;

namespace VinaCent.Blaze.AppCore.AuditLogs
{
    public interface IAuditLogAppService : IApplicationService
    {
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AuditLogDto> GetAsync(long id);

        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long id);

        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AuditLogListDto>> GetAllAsync(PagedAuditLogResultRequestDto input);
    }
}

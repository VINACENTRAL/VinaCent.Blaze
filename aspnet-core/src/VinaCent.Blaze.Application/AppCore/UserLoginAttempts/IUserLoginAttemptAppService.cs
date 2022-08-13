using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.UserLoginAttempts.Dto;

namespace VinaCent.Blaze.AppCore.UserLoginAttempts
{
    /// <summary>
    /// UserLoginAttempts Services
    /// </summary>
    public interface IUserLoginAttemptAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserLoginAttemptDto> GetAsync(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<UserLoginAttemptDto>> GetAllAsync(PagedUserLoginAttemptResultRequestDto input);
    }
}

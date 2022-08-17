using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VinaCent.Blaze.Profiles.Dto;
using VinaCent.Blaze.Roles.Dto;
using VinaCent.Blaze.Users.Dto;

namespace VinaCent.Blaze.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}

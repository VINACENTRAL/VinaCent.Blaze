using Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using VinaCent.Blaze.Profiles.Dto;

namespace VinaCent.Blaze.Profiles
{
    public interface IProfileAppService : IApplicationService
    {
        Task<ProfileDto> GetAsync();

        Task<ProfileDto> UpdateAsync(UpdateProfileDto input);

        Task<bool> ChangePasswordAsync(ChangePasswordDto input);

        Task<ProfileDto> UpdateAvatar(UpdateAvatarDto input);
    }
}

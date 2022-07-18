using Abp.Application.Services;
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

        Task<ProfileDto> UpdateCover(UpdateCoverDto input);

        Task<ProfileDto> UpdateSocialNetwork(UpdateSocialDto input);

        Task<string> SendConfirmCodeAsync(RequestEmailDto input);

        Task<string> ConfirmCodeAsync(ConfirmCodeDto input);

        Task<bool> ChangeEmailAsync(ChangeEmailDto input);
    }
}

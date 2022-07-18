using Abp.Application.Services;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.Utilities.Dto.SlugUtilityDto;

namespace VinaCent.Blaze.AppCore.Utilities
{
    public interface IUtilitiesAppService: IApplicationService
    {
        SlugResultDto GetRenderSlugAsync(SlugRequestDto input);
    }
}

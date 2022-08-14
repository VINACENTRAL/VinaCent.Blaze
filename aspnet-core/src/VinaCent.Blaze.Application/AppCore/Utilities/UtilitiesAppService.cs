using VinaCent.Blaze.AppCore.Utilities.Dto.SlugUtilityDto;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.AppCore.Utilities
{
    public class UtilitiesAppService : BlazeAppServiceBase, IUtilitiesAppService
    {
        public SlugResultDto GetRenderSlugAsync(SlugRequestDto input)
        {
            return new SlugResultDto
            {
                Slug = input.RawString.GenerateSlug(input.MaxLength ?? -1)
            };
        }
    }
}

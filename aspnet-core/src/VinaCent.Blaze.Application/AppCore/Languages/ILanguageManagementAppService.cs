using Abp.Application.Services;
using VinaCent.Blaze.AppCore.Languages.Dto;

namespace VinaCent.Blaze.AppCore.Languages
{
    public interface ILanguageManagementAppService : IAsyncCrudAppService<LanguageDto, int, PagedLanguageResultRequestDto, CreateOrUpdateLanguageDto, CreateOrUpdateLanguageDto>
    {
    }
}

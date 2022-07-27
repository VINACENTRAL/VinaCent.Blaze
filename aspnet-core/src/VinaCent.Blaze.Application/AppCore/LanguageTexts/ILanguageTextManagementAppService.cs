using Abp.Application.Services;
using VinaCent.Blaze.AppCore.LanguageTexts.Dto;

namespace VinaCent.Blaze.AppCore.LanguageTexts
{
    public interface ILanguageTextManagementAppService : IAsyncCrudAppService<LanguageTextDto, long, PagedLanguageTextResultRequestDto, CreateLanguageTextDto, UpdateLanguageTextDto>
    {
    }
}

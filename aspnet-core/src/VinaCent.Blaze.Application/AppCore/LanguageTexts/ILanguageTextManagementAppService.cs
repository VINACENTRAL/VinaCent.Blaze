using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.LanguageTexts.Dto;

namespace VinaCent.Blaze.AppCore.LanguageTexts
{
    public interface ILanguageTextManagementAppService : IAsyncCrudAppService<LanguageTextDto, long, PagedLanguageTextResultRequestDto, CreateLanguageTextDto, UpdateLanguageTextDto>
    {
        Task<GroupLanguageText> GetGroupLanguageTextAsync(long? refLanguageTextId);
        Task SaveGroupLanguageTextAsync(GroupLanguageText input);
        Task<GroupLanguageText> GetGroupLanguageTextAlreadyExistsAsync(GroupLanguageTextRequestInput input);
    }
}

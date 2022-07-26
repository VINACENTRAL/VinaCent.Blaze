using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Localization;
using VinaCent.Blaze.AppCore.LanguageTexts.Dto;

namespace VinaCent.Blaze.AppCore.LanguageTexts
{
    public class LanguageTextManagementAppService : AsyncCrudAppService<ApplicationLanguageText, LanguageTextDto, long, PagedLanguageTextResultRequestDto, CreateOrUpdateLanguageTextDto, CreateOrUpdateLanguageTextDto>, ILanguageTextManagementAppService
    {
        public LanguageTextManagementAppService(IRepository<ApplicationLanguageText, long> repository) : base(repository)
        {
        }
    }
}

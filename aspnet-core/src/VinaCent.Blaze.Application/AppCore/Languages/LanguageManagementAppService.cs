using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Localization;
using VinaCent.Blaze.AppCore.Languages.Dto;

namespace VinaCent.Blaze.AppCore.Languages
{
    public class LanguageManagementAppService : AsyncCrudAppService<ApplicationLanguage, LanguageDto, int, PagedLanguageResultRequestDto, CreateOrUpdateLanguageDto, CreateOrUpdateLanguageDto>, ILanguageManagementAppService
    {
        public LanguageManagementAppService(IRepository<ApplicationLanguage> repository) : base(repository)
        {
        }
    }
}

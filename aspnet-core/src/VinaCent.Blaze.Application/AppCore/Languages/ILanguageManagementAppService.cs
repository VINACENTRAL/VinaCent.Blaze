using Abp.Application.Services;
using Abp.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.Languages.Dto;

namespace VinaCent.Blaze.AppCore.Languages
{
    public interface ILanguageManagementAppService : IAsyncCrudAppService<LanguageDto, int, PagedLanguageResultRequestDto, CreateLanguageDto, UpdateLanguageDto>
    {
        //
        // Summary:
        //     Gets list of all languages available to given tenant (or null for host)
        Task<IReadOnlyList<LanguageDto>> GetLanguagesAsync();

        //
        // Summary:
        //     Gets list of all active languages available to given tenant (or null for host)
        Task<IReadOnlyList<LanguageDto>> GetActiveLanguagesAsync();

        //
        // Summary:
        //     Gets the default language or null for a tenant or the host.
        Task<LanguageDto> GetDefaultLanguageOrNullAsync();

        //
        // Summary:
        //     Sets the default language for a tenant or the host.
        //
        // Parameters:
        //   languageName:
        //     Name of the language.
        Task SetDefaultLanguageAsync(string languageName);
    }
}

using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.Languages.Dto;
using VinaCent.Blaze.Authorization;

namespace VinaCent.Blaze.AppCore.Languages
{
    [AbpAuthorize(PermissionNames.Pages_Languages)]
    public class LanguageManagementAppService : AsyncCrudAppService<ApplicationLanguage, LanguageDto, int, PagedLanguageResultRequestDto, CreateLanguageDto, UpdateLanguageDto>,
        ILanguageManagementAppService
    {
        private readonly IApplicationLanguageManager _applicationLanguageManager;
        private readonly ILanguageManager _languageManager;

        public LanguageManagementAppService(IRepository<ApplicationLanguage> repository, IApplicationLanguageManager applicationLanguageManager, ILanguageManager languageManager) : base(repository)
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;
            _applicationLanguageManager = applicationLanguageManager;
            _languageManager = languageManager;
        }

        public override async Task<LanguageDto> GetAsync(EntityDto<int> input)
        {
            var data = await base.GetAsync(input);
            var defaultLanguageName = (await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId))?.Name ?? _languageManager.GetActiveLanguages().FirstOrDefault(x=>x.IsDefault)?.Name;
            data.IsDefault = data.Name == defaultLanguageName;
            return data;
        }

        public override async Task<PagedResultDto<LanguageDto>> GetAllAsync(PagedLanguageResultRequestDto input)
        {
            var data = await base.GetAllAsync(input);
            var defaultLanguageName = (await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId))?.Name ?? _languageManager.GetActiveLanguages().FirstOrDefault(x => x.IsDefault)?.Name;
            foreach (var item in data.Items)
            {
                item.IsDefault = item.Name == defaultLanguageName;
            }
            return data;
        }

        public override async Task<LanguageDto> CreateAsync(CreateLanguageDto input)
        {
            if ((await GetLanguagesAsync()).Any(l => l.Name == input.Name))
            {
                throw new UserFriendlyException("There is already a language with name = " + input.Name);
            }

            ValidateCultureName(input.Name);

            input.TenantId = AbpSession.TenantId;
            return await base.CreateAsync(input);
        }

        public override async Task<LanguageDto> UpdateAsync(UpdateLanguageDto input)
        {
            var existingLanguageWithSameName =
                    (await GetLanguagesAsync()).FirstOrDefault(l => l.Name == input.Name);
            if (existingLanguageWithSameName != null)
            {
                if (existingLanguageWithSameName.Id != input.Id)
                {
                    throw new UserFriendlyException("There is already a language with name = " + input.Name);
                }
            }

            if (input.TenantId == null && AbpSession.TenantId != null)
            {
                throw new UserFriendlyException("Can not update a host language from tenant");
            }

            ValidateCultureName(input.Name);

            return await base.UpdateAsync(input);
        }

        protected override IQueryable<ApplicationLanguage> CreateFilteredQuery(PagedLanguageResultRequestDto input)
        {
            var standardlizeKeyword = input.Keyword?.ToUpper();
            var query = base.CreateFilteredQuery(input);
            query = query.Where(x => x.TenantId == AbpSession.TenantId);
            query = query.WhereIf(input.IsDisabled != null, x => x.IsDisabled == input.IsDisabled.Value);
            query = query.WhereIf(!input.Keyword.IsNullOrEmpty(), x => x.Name.ToUpper() == standardlizeKeyword || x.DisplayName.Contains(standardlizeKeyword));
            return query;
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var currentLanguage = await GetEntityByIdAsync(input.Id);
            if (currentLanguage == null)
            {
                return;
            }

            if (currentLanguage.TenantId == null && AbpSession.TenantId != null)
            {
                // TODO: Update translate
                throw new UserFriendlyException("Can not delete a host language from tenant!");
            }

            await base.DeleteAsync(input);
        }

        public async Task<IReadOnlyList<LanguageDto>> GetLanguagesAsync()
        {
            var applicationLanguages = await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId);
            return applicationLanguages.Select(MapToEntityDto).ToList();
        }

        public async Task<IReadOnlyList<LanguageDto>> GetActiveLanguagesAsync()
        {
            var applicationLanguages = await _applicationLanguageManager.GetActiveLanguagesAsync(AbpSession.TenantId);
            return applicationLanguages.Select(MapToEntityDto).ToList();
        }

        public async Task<LanguageDto> GetDefaultLanguageOrNullAsync()
        {
            var applicationLanguage = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
            return MapToEntityDto(applicationLanguage);
        }

        public async Task SetDefaultLanguageAsync(string languageName)
        {
            await _applicationLanguageManager.SetDefaultLanguageAsync(AbpSession.TenantId, languageName);
        }

        private void ValidateCultureName(string cultureName)
        {
            var isExist = CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => string.Equals(culture.Name, cultureName, StringComparison.CurrentCultureIgnoreCase));
            if (!isExist)
            {
                // TODO: Update translate
                throw new UserFriendlyException("Language Name was not exist in the world!!!");
            }
        }
    }
}

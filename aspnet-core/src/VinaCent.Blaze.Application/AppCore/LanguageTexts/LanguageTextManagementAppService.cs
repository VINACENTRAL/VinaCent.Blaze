using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.LanguageTexts.Dto;
using VinaCent.Blaze.Authorization;

namespace VinaCent.Blaze.AppCore.LanguageTexts
{
    [AbpAuthorize(PermissionNames.Pages_LanguageTexts)]
    public class LanguageTextManagementAppService : AsyncCrudAppService<ApplicationLanguageText, LanguageTextDto, long, PagedLanguageTextResultRequestDto, CreateLanguageTextDto, UpdateLanguageTextDto>, ILanguageTextManagementAppService
    {
        private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;

        public LanguageTextManagementAppService(IRepository<ApplicationLanguageText, long> repository,
            IApplicationLanguageTextManager applicationLanguageTextManager) : base(repository)
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;
            _applicationLanguageTextManager = applicationLanguageTextManager;
        }

        public override async Task<LanguageTextDto> CreateAsync(CreateLanguageTextDto input)
        {
            await Validate(input.Source, input.LanguageName, input.Key);
            return await base.CreateAsync(input);
        }

        public override async Task<LanguageTextDto> UpdateAsync(UpdateLanguageTextDto input)
        {
            await Validate(input.Source, input.LanguageName, input.Key, input.Id);
            if (input.TenantId == null && AbpSession.TenantId != null)
            {
                throw new UserFriendlyException("Can not update a host language text from tenant");
            }
            return await base.UpdateAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var currentLanguage = await GetEntityByIdAsync(input.Id);
            if (currentLanguage == null)
            {
                return;
            }

            if (currentLanguage.TenantId == null && AbpSession.TenantId != null)
            {
                throw new UserFriendlyException("Can not delete a host language text from tenant!");
            }

            await base.DeleteAsync(input);
        }

        protected override IQueryable<ApplicationLanguageText> CreateFilteredQuery(PagedLanguageTextResultRequestDto input)
        {
            input.Keyword = input.Keyword?.Trim()?.ToUpper();

            var query = base.CreateFilteredQuery(input);
            query = query.Where(x => x.TenantId == AbpSession.TenantId);
            query = query.Where(x => x.Source == input.SourceName);
            query = query.WhereIf(!input.Keyword.IsNullOrEmpty(), x => x.Key.ToUpper() == input.Keyword || x.Value.ToUpper().Contains(input.Keyword));
            query = query.Where(x => x.LanguageName == input.CurrentLanguageName);

            return query;
        }

        public override async Task<PagedResultDto<LanguageTextDto>> GetAllAsync(PagedLanguageTextResultRequestDto input)
        {
            var result = await base.GetAllAsync(input);
            var defCul = CultureInfo.GetCultureInfo(input.DefaultLanguageName);
            foreach (var item in result.Items)
            {
                if (input.CurrentLanguageName == input.DefaultLanguageName)
                {
                    item.DefaultValue = item.Value;
                }
                else
                {
                    item.DefaultValue = _applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, input.SourceName, defCul, item.Key);
                }
            }
            return result;
        }

        private async Task Validate(string source, string languageName, string key, long id = 0)
        {
            var isExists = await Repository.GetAll().AnyAsync(x => x.TenantId == AbpSession.TenantId && x.Source == source && x.LanguageName == languageName && x.Key.ToUpper().Trim() == key && x.Id != id);
            if (isExists)
            {
                throw new UserFriendlyException("There is already a language text in current language with key = " + key);
            }
        }
    }
}

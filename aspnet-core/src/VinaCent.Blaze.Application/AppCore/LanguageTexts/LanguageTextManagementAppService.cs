using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.LanguageTexts.Dto;
using VinaCent.Blaze.AppCore.LanguageTexts.Dto.QuickAction;
using VinaCent.Blaze.Authorization;

namespace VinaCent.Blaze.AppCore.LanguageTexts
{
    [AbpAuthorize(PermissionNames.Pages_LanguageTexts)]
    public class LanguageTextManagementAppService : AsyncCrudAppService<ApplicationLanguageText, LanguageTextDto, long, PagedLanguageTextResultRequestDto, CreateLanguageTextDto, UpdateLanguageTextDto>, ILanguageTextManagementAppService
    {
        private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;
        private readonly ILanguageManager _languageManager;

        public LanguageTextManagementAppService(IRepository<ApplicationLanguageText, long> repository,
            IApplicationLanguageTextManager applicationLanguageTextManager,
            ILanguageManager languageManager) : base(repository)
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;
            _applicationLanguageTextManager = applicationLanguageTextManager;
            _languageManager = languageManager;
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
            query = query.WhereIf(!input.SourceName.IsNullOrWhiteSpace(),
                x => x.Source == input.SourceName);
            query = query.WhereIf(!input.Keyword.IsNullOrEmpty(),
                x => x.Key.ToUpper() == input.Keyword || x.Value.ToUpper().Contains(input.Keyword));
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
                    item.DefaultValue = _applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, item.Source, defCul, item.Key);
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

        public async Task<GroupLanguageText> GetGroupLanguageTextAsync(long? refLanguageTextId)
        {
            var group = new GroupLanguageText
            {
                RefLanguageTextId = refLanguageTextId,
                TenantId = AbpSession.TenantId,
                Key = string.Empty,
                Source = string.Empty,
                Pairs = new List<LanguageTextPair>()
            };

            if (refLanguageTextId.HasValue)
            {
                var refLanguageText = await Repository.GetAsync(refLanguageTextId.Value);

                if (refLanguageText.TenantId != AbpSession.TenantId)
                {
                    throw new UserFriendlyException("You can change language text of another tenant!");
                }

                group = ObjectMapper.Map<GroupLanguageText>(refLanguageText);

                // Reset value
                group.RefLanguageTextId = refLanguageTextId;
                group.Pairs = new List<LanguageTextPair>();

                var allRefSet = await Repository.GetAllListAsync(x => x.TenantId == refLanguageText.TenantId && x.Source == refLanguageText.Source && x.Key == refLanguageText.Key);
                group.Pairs.AddRange(allRefSet.Select(x => ObjectMapper.Map<LanguageTextPair>(x)).ToList());

            }

            group.Pairs.AddRange(_languageManager
                .GetActiveLanguages()
                .Where(x => !group.Pairs.Select(p => p.LanguageName).ToArray().Contains(x.Name))
                .Select(x => new LanguageTextPair { LanguageName = x.Name, Value = "" }));

            return group;
        }

        public async Task SaveGroupLanguageTextAsync(GroupLanguageText input)
        {
            if (input.TenantId != AbpSession.TenantId)
            {
                throw new UserFriendlyException("You can change language text of another tenant!");
            }

            if (input.Pairs.IsNullOrEmpty())
            {
                throw new UserFriendlyException("Please provide value for translation texts");
            }

            if (input.RefLanguageTextId.HasValue)
            {
                // Remove if change source name
                var refLanguageText = await Repository.GetAsync(input.RefLanguageTextId.Value);

                if (refLanguageText.TenantId == AbpSession.TenantId)
                {
                    Repository.Delete(x => x.TenantId == refLanguageText.TenantId && x.Source == refLanguageText.Source && x.Key == refLanguageText.Key);
                }

            }

            // Remove to update translate texts
            await Repository.DeleteAsync(x => x.TenantId == input.TenantId && x.Source == input.Source && x.Key == input.Key);

            var dataset = input.Pairs
                .Select(x => ObjectMapper.Map<ApplicationLanguageText>(x))
                .Select(x => ObjectMapper.Map(input, x));

            foreach (var languageText in dataset)
            {
                await Repository.InsertAsync(languageText);
            }
        }

        [HttpPost]
        public async Task<GroupLanguageText> GetGroupLanguageTextAlreadyExistsAsync(GroupLanguageTextRequestInput input)
        {
            var data = await Repository.FirstOrDefaultAsync(x => x.TenantId == input.TenantId && x.Key == input.Key && x.Source == input.Source);
            return await GetGroupLanguageTextAsync(data?.Id);
        }
        
        
    }
}

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyUnits
{
    public class CurrencyUnitAppService : AsyncCrudAppService<CurrencyUnit, CurrencyUnitDto, Guid, PagedCurrencyUnitResultRequestDto, CreateCurrencyUnitDto, UpdateCurrencyUnitDto>,
        ICurrencyUnitAppService
    {
        public CurrencyUnitAppService(IRepository<CurrencyUnit, Guid> repository) : base(repository)
        {
        }

        protected override IQueryable<CurrencyUnit> ApplySorting(IQueryable<CurrencyUnit> query, PagedCurrencyUnitResultRequestDto input)
        {
            return query.OrderByDescending(x => x.IsDefault).OrderByDescending(x => x.IsActive).ThenBy(x => x.CurrencyNativeName);
        }

        protected override IQueryable<CurrencyUnit> CreateFilteredQuery(PagedCurrencyUnitResultRequestDto input)
        {
            var query = base.CreateFilteredQuery(input);
            query = query.WhereIf(!input.Keyword.IsNullOrEmpty(), x => x.CurrencyNativeName.Contains(input.Keyword) || x.CurrencyEnglishName.Contains(input.Keyword));
            return query;
        }

        public override async Task<CurrencyUnitDto> CreateAsync(CreateCurrencyUnitDto input)
        {
            var result = await base.CreateAsync(input);
            await EnsureOnlyOneDefaultCurrency(result);
            return result;
        }

        public override async Task<CurrencyUnitDto> UpdateAsync(UpdateCurrencyUnitDto input)
        {
            var result = await base.UpdateAsync(input);
            await EnsureOnlyOneDefaultCurrency(result);
            return result;
        }

        private async Task EnsureOnlyOneDefaultCurrency(CurrencyUnitDto current)
        {
            if (current != null && current.Id != Guid.Empty && current.IsActive && current.IsDefault)
            {
                var previousDefault = Repository.GetAllList(x => x.IsDefault && x.Id != current.Id)
                    .Select(x =>
                    {
                        x.IsDefault = false;
                        return x;
                    });
                foreach (var item in previousDefault)
                {
                    await Repository.UpdateAsync(item);
                }
            }
        }

        public async Task<CurrencyUnitDto> GetDefault()
        {
            return MapToEntityDto(await Repository.FirstOrDefaultAsync(x => x.IsDefault));
        }
    }
}

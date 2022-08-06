using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyUnits
{
    public class CurrencyUnitAppService : AsyncCrudAppService<CurrencyUnit, CurrencyUnitDto, Guid, PagedCurrencyUnitResultRequestDto, CreateCurrencyUnitDto, UpdateCurrencyUnitDto>,
        ICurrencyUnitAppService
    {
        public CurrencyUnitAppService(IRepository<CurrencyUnit, Guid> repository) : base(repository)
        {
        }
    }
}

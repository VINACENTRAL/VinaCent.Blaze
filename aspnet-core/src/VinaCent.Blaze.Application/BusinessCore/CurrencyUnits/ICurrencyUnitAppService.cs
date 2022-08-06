using Abp.Application.Services;
using System;
using VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyUnits
{
    public interface ICurrencyUnitAppService : IAsyncCrudAppService<CurrencyUnitDto, Guid, PagedCurrencyUnitResultRequestDto, CreateCurrencyUnitDto, UpdateCurrencyUnitDto>
    {
    }
}

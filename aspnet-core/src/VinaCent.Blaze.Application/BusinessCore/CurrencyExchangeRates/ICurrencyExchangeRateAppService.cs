using Abp.Application.Services;
using System;
using VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates
{
    public interface ICurrencyExchangeRateAppService : IAsyncCrudAppService<CurrencyExchangeRateDto, Guid, PagedCurrencyExchangeRateResultRequestDto, CreateCurrencyExchangeRateDto, UpdateCurrencyExchangeRateDto>
    {
    }
}

using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates
{
    public class CurrencyExchangeRateAppService : AsyncCrudAppService<CurrencyExchangeRate, CurrencyExchangeRateDto, Guid, PagedCurrencyExchangeRateResultRequestDto, CreateCurrencyExchangeRateDto, UpdateCurrencyExchangeRateDto>,
        ICurrencyExchangeRateAppService
    {
        public CurrencyExchangeRateAppService(IRepository<CurrencyExchangeRate, Guid> repository) : base(repository)
        {
        }
    }
}

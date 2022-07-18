using Abp.Application.Services.Dto;
using System;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto
{
    public class PagedExchangeRateHistoryResultRequestDto : PagedResultRequestDto
    {
        public Guid CurrentCurrencyUnitId { get; set; }
    }
}

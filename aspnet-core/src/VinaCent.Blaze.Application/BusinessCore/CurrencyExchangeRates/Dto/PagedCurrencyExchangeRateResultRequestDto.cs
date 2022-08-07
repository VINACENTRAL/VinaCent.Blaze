using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto
{
    public class PagedCurrencyExchangeRateResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

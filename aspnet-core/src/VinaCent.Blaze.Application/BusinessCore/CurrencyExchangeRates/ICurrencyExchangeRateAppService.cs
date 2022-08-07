using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates
{
    public interface ICurrencyExchangeRateAppService : IApplicationService
    {
        Task<PagedResultDto<CurrencyExchangeRateListDto>> GetAllListAsync(PagedCurrencyExchangeRateResultRequestDto input);
        Task<CurrencyExchangeRate> UpdateExchangeRateAsync(UpdateExchangeRateDto input);
        Task<PagedResultDto<CurrencyExchangeRateDto>> GetAllHistoryAsync(PagedExchangeRateHistoryResultRequestDto input);
    }
}

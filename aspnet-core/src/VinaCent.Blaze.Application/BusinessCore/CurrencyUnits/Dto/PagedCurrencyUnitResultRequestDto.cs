using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto
{
    public class PagedCurrencyUnitResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

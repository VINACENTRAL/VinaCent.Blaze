using Abp.Domain.Entities;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto
{
    public class CreateCurrencyExchangeRateDto : IPassivable
    {
        public string ISOCurrencySymbolFrom { get; set; }
        public string ISOCurrencySymbolTo { get; set; }
        public decimal ConvertedValue { get; set; }
        public bool IsActive { get; set; }
    }
}

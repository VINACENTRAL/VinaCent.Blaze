using System;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto
{
    public class UpdateExchangeRateDto
    {
        public Guid CurrencyUnitId { get; set; }
        public decimal ConvertedValue { get; set; }
    }
}

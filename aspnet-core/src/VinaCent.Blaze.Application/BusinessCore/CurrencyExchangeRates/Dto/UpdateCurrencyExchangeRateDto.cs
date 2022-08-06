using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto
{
    public class UpdateCurrencyExchangeRateDto : EntityDto<Guid>, IPassivable
    {
        public string ISOCurrencySymbolFrom { get; set; }
        public string ISOCurrencySymbolTo { get; set; }
        public decimal ConvertedValue { get; set; }
        public bool IsActive { get; set; }
    }
}

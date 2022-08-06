using Abp.Domain.Entities;
using System;
using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto
{
    public class CurrencyExchangeRateDto : FullAuditedEntityDto<Guid>, IPassivable
    {
        public string ISOCurrencySymbolFrom { get; set; }
        public string ISOCurrencySymbolTo { get; set; }
        public decimal ConvertedValue { get; set; }
        public bool IsActive { get; set; }
    }
}

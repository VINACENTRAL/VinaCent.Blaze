using Abp.Domain.Entities;
using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto;
using VinaCent.Blaze.Sessions.Dto;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto
{
    [AutoMap(typeof(CurrencyExchangeRate))]
    public class CurrencyExchangeRateDto : FullAuditedEntityDto<Guid>, IPassivable
    {
        public string ISOCurrencySymbolFrom { get; set; }
        public string ISOCurrencySymbolTo { get; set; }
        public decimal ConvertedValue { get; set; }
        public bool IsActive { get; set; }

        public virtual CurrencyUnitDto From { get; set; }
        public virtual CurrencyUnitDto To { get; set; }

        public virtual string CurrencyFromStr => From.GetMoneyFormat(1);
        public virtual string CurrencyToStr => To.GetValableMoneyFormat(ConvertedValue);

        public virtual UserLoginInfoDto Creator { get; set; }
    }
}

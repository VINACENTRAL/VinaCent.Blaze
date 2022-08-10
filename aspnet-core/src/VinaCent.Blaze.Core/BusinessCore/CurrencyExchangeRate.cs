using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.BusinessCore
{
    [Table(nameof(BusinessCore) + "." + nameof(CurrencyExchangeRate) + "s")]
    public class CurrencyExchangeRate : FullAuditedEntity<Guid>, IPassivable
    {
        public string ISOCurrencySymbolFrom { get; set; }

        public string ISOCurrencySymbolTo { get; set; }

        [Column(TypeName = "decimal(25,12)")]
        public decimal ConvertedValue { get; set; }

        public bool IsActive { get; set; }
    }
}

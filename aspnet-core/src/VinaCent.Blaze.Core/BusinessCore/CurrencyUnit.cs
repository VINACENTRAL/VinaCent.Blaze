using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

namespace VinaCent.Blaze.BusinessCore
{
    [Table(nameof(BusinessCore) + "." + nameof(CurrencyUnit) + "s")]
    public class CurrencyUnit : AuditedEntity<Guid>, IPassivable
    {
        public string CultureName { get; set; }
        public string CurrencyEnglishName { get; set; }
        public string CurrencyNativeName { get; set; }

        public int CurrencyDecimalDigits { get; set; }
        public string CurrencyDecimalSeparator { get; set; }
        public string CurrencyGroupSeparator { get; set; }
        public string CurrencySymbol { get; set; }
        public string ISOCurrencySymbol { get; set; }
        public int CurrencyNegativePattern { get; set; }
        public int CurrencyPositivePattern { get; set; }

        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }

        public static List<CurrencyUnit> GetDefaults()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .DistinctBy(x => x.LCID)
                .Select(ci => new { ri = new RegionInfo(ci.LCID), ci })
                .GroupBy(rc => rc.ri.ISOCurrencySymbol)
                .Select(g =>
                {
                    var tulple = g.First();
                    return new CurrencyUnit
                    {
                        Id = Guid.NewGuid(),
                        CultureName = tulple.ci.Name,
                        CurrencyEnglishName = tulple.ri.CurrencyEnglishName,
                        CurrencyNativeName = tulple.ri.CurrencyNativeName,
                        CurrencyDecimalDigits = tulple.ci.NumberFormat.CurrencyDecimalDigits,
                        CurrencyDecimalSeparator = tulple.ci.NumberFormat.CurrencyDecimalSeparator,
                        CurrencyGroupSeparator = tulple.ci.NumberFormat.CurrencyGroupSeparator,
                        CurrencySymbol = tulple.ci.NumberFormat.CurrencySymbol,
                        ISOCurrencySymbol = tulple.ri.ISOCurrencySymbol,
                        CurrencyNegativePattern = tulple.ci.NumberFormat.CurrencyNegativePattern,
                        CurrencyPositivePattern = tulple.ci.NumberFormat.CurrencyPositivePattern,
                        IsActive = tulple.ri.ISOCurrencySymbol.Equals("VND", StringComparison.OrdinalIgnoreCase) || tulple.ri.ISOCurrencySymbol.Equals("USD", StringComparison.OrdinalIgnoreCase),
                        IsDefault = tulple.ri.ISOCurrencySymbol.Equals("USD", StringComparison.OrdinalIgnoreCase),
                    };
                });
            return cultures.ToList();
        }

        public static string[] CurrencyNegativePatterns = new string[] { "($n)", "-$n", "$-n", "$n-", "(n$)", "-n$", "n-$", "n$-", "-n $", "-$ n", "n $-", "$ n-", "$ -n", "n- $", "($ n)", "(n $)" };
        public static string[] CurrencyPositivePatterns = new string[] { "$n", "n$", "$ n", "n $" };
    }
}

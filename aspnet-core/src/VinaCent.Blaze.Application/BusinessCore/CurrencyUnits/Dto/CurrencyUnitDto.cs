using Abp.Domain.Entities;
using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Globalization;
using System.Data.SqlTypes;
using Newtonsoft.Json.Linq;
using Abp.Extensions;

namespace VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto
{
    [AutoMapFrom(typeof(CurrencyUnit))]
    public class CurrencyUnitDto : AuditedEntityDto<Guid>, IPassivable
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

        public string GetMoneyFormat(decimal input)
        {
            try
            {
                var culture = CultureInfo.GetCultureInfo(CultureName).Clone() as CultureInfo;
                culture.NumberFormat.CurrencyDecimalDigits = CurrencyDecimalDigits;
                culture.NumberFormat.CurrencyDecimalSeparator = CurrencyDecimalSeparator;
                culture.NumberFormat.CurrencyGroupSeparator = CurrencyGroupSeparator;
                culture.NumberFormat.CurrencySymbol = CurrencySymbol;
                culture.NumberFormat.CurrencyNegativePattern = CurrencyNegativePattern;
                culture.NumberFormat.CurrencyPositivePattern = CurrencyPositivePattern;

                return input.ToString("C", culture);
            }
            catch
            {
                return CultureName;
            }
        }

        public string GetValableMoneyFormat(decimal input)
        {
            try
            {
                var culture = CultureInfo.GetCultureInfo(CultureName).Clone() as CultureInfo;
                culture.NumberFormat.CurrencyDecimalDigits = CurrencyDecimalDigits;
                culture.NumberFormat.CurrencyDecimalSeparator = CurrencyDecimalSeparator;
                culture.NumberFormat.CurrencyGroupSeparator = CurrencyGroupSeparator;
                culture.NumberFormat.CurrencySymbol = CurrencySymbol;
                culture.NumberFormat.CurrencyNegativePattern = CurrencyNegativePattern;
                culture.NumberFormat.CurrencyPositivePattern = CurrencyPositivePattern;

                var inps = input.ToString(CultureInfo.InvariantCulture).Split('.');
                if (inps.Length > 1)
                {
                    inps[1] = inps[1].TrimEnd('0');
                    if (!inps[1].IsNullOrEmpty())
                    {
                        inps[1] = inps[1][..Math.Min(inps[1].Length, 150)];
                        var shouldLength = inps[1].Length - inps[1].TrimStart('0').Length + culture.NumberFormat.CurrencyDecimalDigits;
                        culture.NumberFormat.CurrencyDecimalDigits = Math.Min(inps[1].Length, shouldLength);
                    }
                }

                return input.ToString("C", culture);
            }
            catch
            {
                return CultureName;
            }
        }
    }
}

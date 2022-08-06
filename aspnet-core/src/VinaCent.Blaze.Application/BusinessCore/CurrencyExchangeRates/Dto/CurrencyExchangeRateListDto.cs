using Abp.Domain.Entities;
using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System.Globalization;
using Abp.Extensions;
using VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto;
using System.Data.SqlTypes;

namespace VinaCent.Blaze.BusinessCore.CurrencyExchangeRates.Dto
{
    /// <summary>
    /// Although using that name but base on Currency Unit
    /// </summary>
    [AutoMap(typeof(CurrencyUnit))]
    public class CurrencyExchangeRateListDto : EntityDto<Guid>, IPassivable
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

        /// <summary>
        /// Crruent: VND - Default: USD
        /// Current was not Default currency.
        /// Now value will be calculate by: 1 USD = 23.000 VND 
        /// </summary>
        public decimal? ValueBaseOnOneDefault { get; set; }

        /// <summary>
        /// Crruent: VND - Default: USD
        /// Current was not Default currency.
        /// Now value will be calculate by: 1 VND = 0.000043 USD 
        /// </summary>
        public decimal? ValueBaseOnOneCurrent { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Time that lastest Exchange rate was updated
        /// </summary>
        public DateTime? CreationTime { get; set; } = null;

        public virtual string CreationTimeStr
        {
            get
            {
                var currentCultureDatetimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                var parttern = currentCultureDatetimeFormat.ShortTimePattern + " - " + currentCultureDatetimeFormat.ShortDatePattern;
                return CreationTime?.ToString(parttern);
            }
        }

        private string GetMoneyFormat(decimal? input)
        {
            var culture = CultureInfo.GetCultureInfo(CultureName).Clone() as CultureInfo;
            culture.NumberFormat.CurrencyDecimalDigits = CurrencyDecimalDigits;
            culture.NumberFormat.CurrencyDecimalSeparator = CurrencyDecimalSeparator;
            culture.NumberFormat.CurrencyGroupSeparator = CurrencyGroupSeparator;
            culture.NumberFormat.CurrencySymbol = CurrencySymbol;
            culture.NumberFormat.CurrencyNegativePattern = CurrencyNegativePattern;
            culture.NumberFormat.CurrencyPositivePattern = CurrencyPositivePattern;

            if (input != null)
            {
                var inps = input.Value.ToString(CultureInfo.InvariantCulture).Split('.');
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
            }

            return input?.ToString("C", culture) ?? string.Empty;
        }

        #region Standalized data
        public virtual CurrencyUnitDto Default { get; set; }

        public virtual string StandalizedValueBaseOnOneDefault => GetMoneyFormat(ValueBaseOnOneDefault);

        public virtual string ExchangeRateBaseOnOneDefaultDesciption => StandalizedValueBaseOnOneDefault.IsNullOrEmpty() ? string.Empty : $"{Default.GetValableMoneyFormat(1)} ➤ {StandalizedValueBaseOnOneDefault}";

        public virtual string StandalizedValueBaseOnOneCurrent => ValueBaseOnOneCurrent != null ? Default.GetValableMoneyFormat(ValueBaseOnOneCurrent.Value) : string.Empty;

        public virtual string ExchangeRateBaseOnOneCurrentDesciption => StandalizedValueBaseOnOneCurrent.IsNullOrEmpty() ? string.Empty : $"{GetMoneyFormat(1)} ➤ {StandalizedValueBaseOnOneCurrent}";
        #endregion

        //private static ILocalizableString L(string name)
        //{
        //    return new LocalizableString(name, BlazeConsts.LocalizationSourceName);
        //}
    }
}

﻿using Abp.Domain.Entities;

namespace VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto
{
    public class CreateCurrencyUnitDto : IPassivable
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
    }
}

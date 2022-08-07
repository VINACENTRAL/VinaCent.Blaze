using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.BusinessCore.CurrencyUnits.Dto
{
    [AutoMap(typeof(CurrencyUnit))]
    [AutoMapFrom(typeof(CurrencyUnitDto))]
    public class UpdateCurrencyUnitDto : EntityDto<Guid>, IPassivable
    {
        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CultureName)]
        public string CultureName { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrencyEnglishName)]
        public string CurrencyEnglishName { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrencyNativeName)]
        public string CurrencyNativeName { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrencyDecimalDigits)]
        public int CurrencyDecimalDigits { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrencyDecimalSeparator)]
        public string CurrencyDecimalSeparator { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrencyGroupSeparator)]
        public string CurrencyGroupSeparator { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrencySymbol)]
        public string CurrencySymbol { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ISOCurrencySymbol)]
        public string ISOCurrencySymbol { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrencyNegativePattern)]
        public int CurrencyNegativePattern { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CurrencyPositivePattern)]
        public int CurrencyPositivePattern { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IsActive)]
        public bool IsActive { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IsDefault)]
        public bool IsDefault { get; set; }
    }
}

using Abp.AutoMapper;
using Abp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Profiles.Dto
{
    [AutoMap(typeof(User))]
    public class UpdateProfileDto
    {

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Name)]
        public string Name { get; set; }

        [AppRequired]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Surname)]
        public string Surname { get; set; }

        [AppRequired]
        public string ConcurrencyStamp { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.City)]
        public string City { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.State)]
        public string State { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Country)]
        public string Country { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ZipCode)]
        [StringLength(6)]
        public string ZipCode { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Bio)]
        public string Description { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Birthday)]
        public DateTime? Birthday { get; set; }

        public virtual string BirthdayStr { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IdentityCardNumber)]
        public string IdentityCardNumber { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.AddressLine1)]
        public string AddressLine1 { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.AddressLine2)]
        public string AddressLine2 { get; set; }
    }
}

using Abp.AutoMapper;
using Abp.Localization;
using System;
using VinaCent.Blaze.Authorization.Users;

namespace VinaCent.Blaze.Profiles.Dto
{
    [AutoMapFrom(typeof(User))]
    public class ProfileDto
    {
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.UserName)]
        public string UserName { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EmailAddress)]
        public string EmailAddress { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Name)]
        public string Name { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Surname)]
        public string Surname { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string AuthenticationSource { get; set; }

        public string ConcurrencyStamp { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.City)]
        public string City { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Country)]
        public string Country { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ZipCode)]
        public string ZipCode { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Description)]
        public string Description { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.CreationTime)]
        public DateTime CreationTime { get; set; }

        public bool HasPassword { get; set; }
    }
}

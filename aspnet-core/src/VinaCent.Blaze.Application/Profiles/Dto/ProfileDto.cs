﻿using Abp.AutoMapper;
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

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Country)]
        public string Country { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.State)]
        public string State { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.City)]
        public string City { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ZipCode)]
        public string ZipCode { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Bio)]
        public string Description { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Birthday)]
        public DateTime? Birthday { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IdentityCardNumber)]
        public string IdentityCardNumber { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.AddressLine1)]
        public string AddressLine1 { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.AddressLine2)]
        public string AddressLine2 { get; set; }

        public bool HasPassword { get; set; }
    }
}

using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Helpers;

namespace VinaCent.Blaze.Profiles.Dto
{
    [AutoMapFrom(typeof(User))]
    public class ProfileDto
    {

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.UserName)]
        public string UserName { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EmailAddress)]
        public string EmailAddress { get; set; }

        public virtual string MaskedEmailAddress
        {
            get
            {
                return EmailAddress.MaskHiddingEmailAddress();
            }
        }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Name)]
        public string Name { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Surname)]
        public string Surname { get; set; }

        public virtual string FullName
        {
            get
            {
                string fullName;
                var styleName_SureName_Name = new List<string> { "vi", "vi-VN" };

                if (styleName_SureName_Name.Contains(CultureInfo.CurrentUICulture.Name))
                {
                    fullName = Surname + " " + Name;
                }
                else
                {
                    fullName = Name + " " + Surname;
                }

                fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fullName.Trim());

                return fullName;
            }
        }

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

        public virtual string BirthdayStr
        {
            get
            {
                var currentCultureDatetimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                var pattern = currentCultureDatetimeFormat.ShortDatePattern;
                return Birthday?.ToString(pattern);
            }
        }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IdentityCardNumber)]
        public string IdentityCardNumber { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.AddressLine1)]
        public string AddressLine1 { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.AddressLine2)]
        public string AddressLine2 { get; set; }

        public string Avatar { get; set; }
        
        public string Cover { get; set; }
        
        public bool HasPassword { get; set; }

        public string ListSocialNetworkRawJson { get; set; }

        public List<SocialNetwork> SocialNetworks
        {
            get
            {
                if (ListSocialNetworkRawJson.IsNullOrWhiteSpace()) return new List<SocialNetwork>();
                return JsonConvert.DeserializeObject<List<SocialNetwork>>(ListSocialNetworkRawJson);
            }
            set { ListSocialNetworkRawJson = JsonConvert.SerializeObject(this); }
        }
    }
}

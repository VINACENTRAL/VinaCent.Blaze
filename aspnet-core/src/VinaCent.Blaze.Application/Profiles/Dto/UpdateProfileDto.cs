using Abp.AutoMapper;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.Authorization.Users;

namespace VinaCent.Blaze.Profiles.Dto
{
    [AutoMap(typeof(User))]
    public class UpdateProfileDto
    {
        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.UserName)]
        public string UserName { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Name)]
        public string Name { get; set; }

        [Required]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Surname)]
        public string Surname { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.PhoneNumber)]
        [RegularExpression(@"^\+(?:[0-9]●?){6,14}[0-9]$")]
        public string PhoneNumber { get; set; }

        [Required]
        public string ConcurrencyStamp { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.City)]
        public string City { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Country)]
        public string Country { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.ZipCode)]
        [StringLength(maximumLength:6, MinimumLength = 5)]
        public string ZipCode { get; set; }

        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Description)]
        public string Description { get; set; }
    }
}

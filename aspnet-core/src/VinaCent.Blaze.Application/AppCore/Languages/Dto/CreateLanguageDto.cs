using Abp.Domain.Entities;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.Languages.Dto
{
    // Ref: https://github.com/aspnetboilerplate/aspnetboilerplate/blob/059db7626b3642114b7a2ba7d15b6a14304640dd/src/Abp.Zero.Common/Localization/ApplicationLanguage.cs
    [AutoMapTo(typeof(ApplicationLanguage))]
    public class CreateLanguageDto : IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the name of the culture, like "en" or "en-US".
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguage.MaxNameLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Name)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [AppRequired]
        [AppStringLength(ApplicationLanguage.MaxDisplayNameLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.DisplayName)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        [AppStringLength(ApplicationLanguage.MaxIconLength)]
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.Icon)]
        public string Icon { get; set; }

        /// <summary>
        /// Is this language active. Inactive languages are not get by <see cref="IApplicationLanguageManager"/>.
        /// </summary>
        [AbpDisplayName(BlazeConsts.LocalizationSourceName, LKConstants.IsDisabled)]
        public bool IsDisabled { get; set; }

    }
}

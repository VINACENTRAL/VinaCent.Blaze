using Abp.Domain.Entities;
using Abp.Application.Services.Dto;
using Abp.Localization;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace VinaCent.Blaze.AppCore.Languages.Dto
{
    // Ref: https://github.com/aspnetboilerplate/aspnetboilerplate/blob/059db7626b3642114b7a2ba7d15b6a14304640dd/src/Abp.Zero.Common/Localization/ApplicationLanguage.cs
    [AutoMap(typeof(ApplicationLanguage))]
    public class CreateOrUpdateLanguageDto : EntityDto, IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the name of the culture, like "en" or "en-US".
        /// </summary>
        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [Required]
        [StringLength(ApplicationLanguage.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        [StringLength(ApplicationLanguage.MaxIconLength)]
        public string Icon { get; set; }

        /// <summary>
        /// Is this language active. Inactive languages are not get by <see cref="IApplicationLanguageManager"/>.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Creates a new <see cref="ApplicationLanguage"/> object.
        /// </summary>
        public CreateOrUpdateLanguageDto()
        {
        }

        public CreateOrUpdateLanguageDto(int? tenantId, string name, string displayName, string icon = null, bool isDisabled = false)
        {
            TenantId = tenantId;
            Name = name;
            DisplayName = displayName;
            Icon = icon;
            IsDisabled = isDisabled;
        }

        public LanguageInfo ToLanguageInfo()
        {
            return new LanguageInfo(Name, DisplayName, Icon, isDisabled: IsDisabled);
        }
    }
}

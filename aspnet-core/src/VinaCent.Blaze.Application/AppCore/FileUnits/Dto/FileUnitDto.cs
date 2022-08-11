using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using VinaCent.Blaze.Helpers;
using VinaCent.Blaze.Users.Dto;

namespace VinaCent.Blaze.AppCore.FileUnits.Dto
{
    [AutoMap(typeof(FileUnit))]
    public class FileUnitDto : AuditedEntityDto<Guid>, IMayHaveTenant
    {
        /// <summary>
        /// Folder parent id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Parent foler
        /// </summary>
        public virtual FileUnitDto Parent { get; set; }

        /// <summary>
        /// Real file name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for file or folder
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Current Tenant of User
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// This unit is folder or file
        /// </summary>
        public bool IsFolder { get; set; }

        /// <summary>
        /// Is static folder and its child, you can rename, move, edit or delete all of it
        /// </summary>
        public bool IsStatic { get; set; }

        #region Extend propperties

        /// <summary>
        /// Gets an instance of the parent directory
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Gets the extension part of the file name, including the leading dot . even if it is the entire file name, or an empty string if no extension is present.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Get file name with out extension (Ex: pic.png -> pic)
        /// </summary>
        public string NameWithoutExtension { get; set; }

        /// <summary>
        /// Gets the full path of the directory or file
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets the size, in bytes, of the current file.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Real path of file/folder in physical file system
        /// </summary>
        public string PhysicalPath { get; set; }

        public virtual string CreationTimeStr
        {
            get
            {
                var currentCultureDatetimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                var pattern = currentCultureDatetimeFormat.ShortTimePattern + " - " + currentCultureDatetimeFormat.ShortDatePattern;
                return CreationTime.ToString(pattern);
            }
        }

        public virtual string LastModificationTimeStr
        {
            get
            {
                var currentCultureDatetimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                var pattern = currentCultureDatetimeFormat.ShortTimePattern + " - " + currentCultureDatetimeFormat.ShortDatePattern;
                return LastModificationTime?.ToString(pattern);
            }
        }

        /// <summary>
        /// Path for download or preview
        /// </summary>
        [NotMapped]
        public string ResourcePath => StringHelper.TrueCombine("/resources", FullName);

        #endregion

        [CanBeNull] public UserDto Creator { get; set; }
    }
}

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinaCent.Blaze.AppCore.FileUnits
{
    [Table(nameof(AppCore) + "." + nameof(FileUnits))]
    public class FileUnit : AuditedEntity<Guid>, IMayHaveTenant
    {
        /// <summary>
        /// Folder parent id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Parent foler
        /// </summary>
        [ForeignKey(nameof(ParentId))]
        public virtual FileUnit Parent { get; set; }

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
    }
}

using Abp.AutoMapper;
using System;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.FileUnits.Dto
{
    [AutoMap(typeof(FileUnit))]
    public class CreateDirectoryDto
    {
        /// <summary>
        /// Folder parent id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Folder name
        /// </summary>
        [AppRequired]
        [AppRegex(@"^[\w\-. ]+$")]
        public string Name { get; set; }

        /// <summary>
        /// Description for file or folder
        /// </summary>
        public string Description { get; set; }
    }
}

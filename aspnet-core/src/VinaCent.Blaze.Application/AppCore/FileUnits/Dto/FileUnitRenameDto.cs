using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.FileUnits.Dto
{
    [AutoMap(typeof(FileUnit))]
    [AutoMapFrom(typeof(FileUnitDto))]
    public class FileUnitRenameDto : EntityDto<Guid>
    {
        /// <summary>
        /// Real file name
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

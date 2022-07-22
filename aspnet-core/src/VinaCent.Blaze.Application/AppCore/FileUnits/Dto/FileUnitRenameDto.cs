using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinaCent.Blaze.AppCore.FileUnits.Dto
{
    [AutoMap(typeof(FileUnit))]
    public class FileUnitRenameDto : EntityDto<Guid>
    {
        /// <summary>
        /// Real file name
        /// </summary>
        [Required]      
        [RegularExpression(@"^[\w\-. ]+$")]
        public string Name { get; set; }

        /// <summary>
        /// Description for file or folder
        /// </summary>
        public string Description { get; set; }
    }
}

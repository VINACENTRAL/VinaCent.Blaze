using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinaCent.Blaze.AppCore.FileUnits.Dto
{
    public class PagedFileUnitResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        /// <summary>
        /// Is Find folder or file
        /// </summary>
        public bool? IsFolder { get; set; }

        /// <summary>
        /// Current parent directory
        /// </summary>
        public string Directory { get; set; }
    }
}

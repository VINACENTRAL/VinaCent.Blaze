using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.AppCore.FileUnits.Dto
{
    [AutoMap(typeof(FileUnit))]
    public class UploadFileUnitDto
    {
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Description for file or folder
        /// </summary>
        public string Description { get; set; }

        [AppRequired] 
        public IFormFile File { get; set; }
    }
}

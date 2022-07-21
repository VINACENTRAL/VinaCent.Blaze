using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.FileUnits.Dto;
using VinaCent.Blaze.Authorization.Users;

namespace VinaCent.Blaze.AppCore.FileUnits
{
    public class FileUnitAppService : BlazeAppServiceBase, IFileUnitAppService
    {
        private readonly IRepository<FileUnit, Guid> _repository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHostEnvironment _hostEnvironment;

        /// <summary>
        /// Physical directory to store system files
        /// </summary>
        private const string ContentPhysicalDirectory = "contents";

        /// <summary>
        /// Virtual root directory to contains all user files
        /// </summary>
        private const string UsersDirName = "personal";

        /// <summary>
        /// Virtual root directory to contain all system's runtime files
        /// </summary>
        private const string ContentsDirName = "contents";

        public static readonly string[] StaticDirName = { UsersDirName, ContentsDirName };

        public Task<FileUnitDto> CreateDirectoryAsync(CreateDirectoryDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<FileUnitDto>> GetAllAsync(PagedFileUnitResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        public Task<FileUnitDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<FileUnitDto> MoveAsync(Guid id, string directory)
        {
            throw new NotImplementedException();
        }

        public Task<FileUnitDto> RenameAsync(UpdateFileUnitDto input)
        {
            throw new NotImplementedException();
        }

        public Task<FileUnitDto> UploadFileAsync(UploadFileUnitDto input)
        {
            throw new NotImplementedException();
        }
    }
}

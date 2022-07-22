using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.FileUnits.Dto;

namespace VinaCent.Blaze.AppCore.FileUnits
{
    public interface IFileUnitAppService : IApplicationService
    {
        public string GetAppContentPath { get; }
        public string GetCurrentTenantContentPath { get; }
        public string GetCurrentUserContentPath { get; }
        public string GetTodayContentPath { get; }
        Task<FileUnitDto> GetAsync(Guid id);
        Task<PagedResultDto<FileUnitDto>> GetAllAsync(PagedFileUnitResultRequestDto input);
        Task<List<FileUnitDto>> GetAllParentAsync(string directory);
        Task<FileUnitDto> GetByFullName(string fullName);
        Task<FileUnitDto> GetParentAsync(string directory);
        Task<FileUnitDto> UploadFileAsync(UploadFileUnitDto input);
        Task<FileUnitDto> CreateDirectoryAsync(CreateDirectoryDto input);
        Task<FileUnitDto> RenameAsync(FileUnitRenameDto input);
        Task<FileUnitDto> MoveAsync(Guid id, Guid directoryId);
        Task DeleteAsync(Guid id);
    }
}

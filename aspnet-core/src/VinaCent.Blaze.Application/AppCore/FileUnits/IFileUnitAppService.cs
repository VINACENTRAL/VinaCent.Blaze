using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.FileUnits.Dto;

namespace VinaCent.Blaze.AppCore.FileUnits
{
    public interface IFileUnitAppService : IApplicationService
    {
        Task<FileUnitDto> GetAsync(Guid id);
        Task<PagedResultDto<FileUnitDto>> GetAllAsync(PagedFileUnitResultRequestDto input);
        Task<FileUnitDto> UploadFileAsync(UploadFileUnitDto input);
        Task<FileUnitDto> CreateDirectoryAsync(CreateDirectoryDto input);
        Task<FileUnitDto> RenameAsync(FileUnitRenameDto input);
        Task<FileUnitDto> MoveAsync(Guid id, Guid directoryId);
        Task DeleteAsync(Guid id);
    }
}

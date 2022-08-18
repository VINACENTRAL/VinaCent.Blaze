using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.FileUnits.Dto;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Helpers;
using VinaCent.Blaze.Users.Dto;

namespace VinaCent.Blaze.AppCore.FileUnits
{
    [AbpAuthorize(PermissionNames.Pages_FileManagement)]
    public class FileUnitAppService : BlazeAppServiceBase, IFileUnitAppService
    {
        private readonly FileUnitManager _fileUnitManager;

        private readonly IRepository<FileUnit, Guid> _repository;
        private readonly IRepository<User, long> _userRepository;


        public FileUnitAppService(
            IRepository<FileUnit, Guid> repository,
            IRepository<User, long> userRepository,
            FileUnitManager fileUnitManager)
        {
            _repository = repository;
            _userRepository = userRepository;
            _fileUnitManager = fileUnitManager;
        }

        public async Task<FileUnitDto> GetAsync(Guid id) => MapToEntityDto(await _fileUnitManager.GetById(id));

        public async Task<PagedResultDto<FileUnitDto>> GetAllAsync(PagedFileUnitResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);
            query = ApplySorting(query);

            var totalCount = await query.CountAsync();

            query = ApplyPaging(query, input);
            var items = MapToEntityDto(query.ToList());
            foreach (var item in items)
            {
                item.IsStatic = FileUnitConsts.StaticDirName.Any(x =>
                    item.FullName.EnsureStartsWith(StringHelper.FileSeparator.First())
                        .StartsWith(x.EnsureStartsWith(StringHelper.FileSeparator.First())));
                if (item.CreatorUserId is not > 0) continue;
                var creator = await _userRepository.GetAsync(item.CreatorUserId.Value);
                item.Creator = ObjectMapper.Map<UserDto>(creator);
            }

            return new PagedResultDto<FileUnitDto>(totalCount, items);
        }

        public async Task<FileUnitDto> UploadFileAsync([FromForm] UploadFileUnitDto input) => MapToEntityDto(await _fileUnitManager.UploadFileAsync(MapToEntity(input), input.File));

        public async Task<FileUnitDto> CreateDirectoryAsync(CreateDirectoryDto input) => MapToEntityDto(await _fileUnitManager.CreateDirectoryAsync(MapToEntity(input)));

        public async Task<FileUnitDto> RenameAsync(FileUnitRenameDto input) => MapToEntityDto(await _fileUnitManager.RenameAsync(input.Id, input.Name, input.Description));

        public async Task<FileUnitDto> MoveAsync(Guid id, Guid directoryId) => MapToEntityDto(await _fileUnitManager.MoveAsync(id, directoryId));

        public Task DeleteAsync(Guid id) => _fileUnitManager.DeleteAsync(id);



        private IQueryable<FileUnit> CreateFilteredQuery(PagedFileUnitResultRequestDto input)
        {
            var query = _repository.GetAll();
            query = query.WhereIf(input.IsFolder != null, x => x.IsFolder == input.IsFolder);
            query = query.WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
                x => x.Name.Contains(input.Keyword) || x.Description.Contains(input.Keyword));
            query = query.WhereIf(!input.Directory.IsNullOrWhiteSpace(),
                x => x.Directory.ToLower() == input.Directory.ToLower());
            query = query.WhereIf(input.Directory.IsNullOrWhiteSpace(),
                x => x.Directory == StringHelper.FileSeparator);
            return query;
        }

        private IQueryable<FileUnit> ApplyPaging(IQueryable<FileUnit> query,
            PagedFileUnitResultRequestDto input)
        {
            return query.Skip(input.SkipCount)
                .Take(input.MaxResultCount);
        }

        private IQueryable<FileUnit> ApplySorting(IQueryable<FileUnit> query)
        {
            query = query.OrderByDescending(x => x.IsFolder)
                .ThenBy(x => x.CreationTime)
                .ThenBy(x => x.Name);
            return query;
        }

        private FileUnitDto MapToEntityDto(FileUnit input)
        {
            return ObjectMapper.Map<FileUnitDto>(input);
        }

        private List<FileUnitDto> MapToEntityDto(List<FileUnit> input)
        {
            return ObjectMapper.Map<List<FileUnitDto>>(input);
        }

        private FileUnit MapToEntity<T>(T input)
        {
            return ObjectMapper.Map<FileUnit>(input);
        }
    }
}
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.FileUnits.Dto;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.Helpers;
using VinaCent.Blaze.Users.Dto;

namespace VinaCent.Blaze.AppCore.FileUnits
{
    [AbpAuthorize(PermissionNames.Pages_FileManagement)]
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

        /// <summary>
        /// Root folder for app file system
        /// </summary>
        /// <returns></returns>
        public string GetAppContentPath
        {
            get
            {
                var path = StringHelper.TrueCombine(_webHostEnvironment.WebRootPath, ContentPhysicalDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        /// <summary>
        /// Current content folder of current tenant
        /// </summary>
        /// <returns></returns>
        public string GetCurrentTenantContentPath
        {
            get
            {
                var currentTenantContentDirectory =
                    AbpSession.TenantId == null ? "root" : $"tenant_{AbpSession.TenantId}";
                var path = StringHelper.TrueCombine(GetAppContentPath, currentTenantContentDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        /// <summary>
        /// Get content folder of current User
        /// </summary>
        public string GetCurrentUserContentPath
        {
            get
            {
                var currentUserContentDirectory =
                    AbpSession.UserId == null ? "anonymous" : $"user_{AbpSession.UserId}";
                var path = StringHelper.TrueCombine(GetCurrentTenantContentPath, currentUserContentDirectory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        /// <summary>
        /// Get today content folder
        /// </summary>
        public string GetTodayContentPath
        {
            get
            {
                var year = DateTime.Now.ToString("yyyy");
                var month = DateTime.Now.ToString("MM");
                var day = DateTime.Now.ToString("dd");

                var path = StringHelper.TrueCombine(GetCurrentUserContentPath, year, month, day);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        public FileUnitAppService(
            IRepository<FileUnit, Guid> repository,
            IRepository<User, long> userRepository,
            IWebHostEnvironment webHostEnvironment,
            IHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<FileUnitDto> GetAsync(Guid id)
        {
            var entityDto = MapToEntityDto(await GetFileUnit(id));
            if (entityDto.CreatorUserId != null)
            {
                var creator = await _userRepository.GetAsync(entityDto.CreatorUserId.Value);
                entityDto.Creator = ObjectMapper.Map<UserDto>(creator);
            }

            return entityDto;
        }

        public async Task<List<FileUnitDto>> GetAllParentAsync(string directory)
        {
            var parentDirectories = new List<FileUnitDto>();
            if (directory.IsNullOrEmpty())
            {
                return parentDirectories;
            }

            FileUnit currentDirectory;
            do
            {
                currentDirectory = await _repository.FirstOrDefaultAsync(x =>
                    x.FullName == directory);
                if (currentDirectory == null)
                {
                    break;
                }

                directory = currentDirectory.Directory;

                parentDirectories.Add(MapToEntityDto(currentDirectory));
            } while (!currentDirectory.Directory.IsNullOrEmpty() && currentDirectory.Directory != "/");

            parentDirectories.Reverse();
            return parentDirectories;
        }

        [AbpAllowAnonymous]
        public async Task<FileUnitDto> GetByFullName(string fullName)
        {
            if (fullName.IsNullOrWhiteSpace())
                return null;
            if (!fullName.StartsWith("/"))
                fullName = "/" + fullName;
            var file = await _repository.FirstOrDefaultAsync(x => x.FullName == fullName);
            if (file == null)
                return null;
            var entityDto = MapToEntityDto(file);
            if (entityDto.CreatorUserId != null)
            {
                var creator = await _userRepository.GetAsync(entityDto.CreatorUserId.Value);
                entityDto.Creator = ObjectMapper.Map<UserDto>(creator);
            }

            // If is a folder => Return it
            if (entityDto.IsFolder)
            {
                return entityDto;
            }

            if (File.Exists(entityDto.PhysicalPath)) return entityDto;

            // Check if file not found, also remove in virtual file manager
            // Remove in virtual file
            if (_hostEnvironment.IsProduction())
            {
                await _repository.DeleteAsync(entityDto.Id);
            }

            return null;
        }

        public async Task<FileUnitDto> GetParentAsync(string directory)
        {
            if (directory.IsNullOrEmpty())
            {
                throw new UserFriendlyException(L(LKConstants.YourParentDirectoryPathNotCorrect));
            }

            var currentDirectory = await _repository.FirstOrDefaultAsync(x =>
                x.FullName == directory);
            if (currentDirectory == null)
            {
                throw new UserFriendlyException(L(LKConstants.ParentDirectoryWasNotExists));
            }

            return MapToEntityDto(currentDirectory);
        }

        public async Task<PagedResultDto<FileUnitDto>> GetAllAsync(PagedFileUnitResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);
            query = ApplySorting(query);

            var totalCount = await query.CountAsync();

            query = ApplyPaging(query, input);
            var items = MapToEntityDto(query.ToList());
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < items.Count; i++)
            {
                var entity = items[i];
                if (entity.CreatorUserId is not > 0) continue;
                var creator = await _userRepository.GetAsync(entity.CreatorUserId.Value);
                entity.Creator = ObjectMapper.Map<UserDto>(creator);
            }

            return new PagedResultDto<FileUnitDto>(totalCount, items);
        }

        public async Task<FileUnitDto> CreateDirectoryAsync(CreateDirectoryDto input)
        {
            var fileUnit = ObjectMapper.Map<FileUnit>(input);
            fileUnit.Id = Guid.NewGuid();

            NameValidation(input.Name);

            fileUnit.TenantId = AbpSession.TenantId;
            fileUnit.IsFolder = true;

            if (input.ParentId != null && input.ParentId != Guid.Empty)
            {
                var directory = await GetAsync(input.ParentId.Value);
                fileUnit.Directory = directory.FullName;
            }
            else
            {
                fileUnit.Directory = "/";
            }

            fileUnit.Name = input.Name;
            fileUnit.Extension = "";
            fileUnit.NameWithoutExtension = input.Name;
            fileUnit.FullName = StringHelper.TrueCombine(fileUnit.Directory, fileUnit.Name);
            fileUnit.Length = 0;

            fileUnit.PhysicalPath = "";

            fileUnit = RollUniqueName(fileUnit);
            fileUnit = await _repository.InsertAsync(fileUnit);

            return MapToEntityDto(fileUnit);
        }

        [AbpAuthorize]
        public async Task<FileUnitDto> UploadFileAsync([FromForm] UploadFileUnitDto input)
        {
            var fileUnit = MapToEntity(input);
            fileUnit.Id = Guid.NewGuid();

            if (input.File is not { Length: > 0 })
            {
                throw new UserFriendlyException(L(LKConstants.YouMustChooseAFileToUpload));
            }

            var allowedMaxFileSize = Convert.ToInt16(await SettingManager.GetSettingValueAsync(AppSettingNames.AllowedMaxFileSize));//kb
            var allowedUploadFormats = (await SettingManager.GetSettingValueAsync(AppSettingNames.AllowedUploadFormats))
                ?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

            fileUnit.TenantId = AbpSession.TenantId;
            fileUnit.IsFolder = false;

            if (input.ParentId != null && input.ParentId != Guid.Empty)
            {
                var directory = await GetAsync(input.ParentId.Value);
                fileUnit.Directory = directory.FullName;
            }
            else
            {
                fileUnit.Directory = "/";
            }

            fileUnit.Name = Path.GetFileName(input.File.FileName);
            fileUnit.Extension = Path.GetExtension(input.File.FileName);

            if (allowedUploadFormats == null || !allowedUploadFormats.Contains(fileUnit.Extension))
            {
                throw new UserFriendlyException(L(LKConstants.NotValidFormat));
            }

            fileUnit.NameWithoutExtension = Path.GetFileNameWithoutExtension(input.File.FileName);
            fileUnit.FullName = StringHelper.TrueCombine(fileUnit.Directory, fileUnit.Name);
            fileUnit.Length = input.File.Length;

            if (fileUnit.Length > allowedMaxFileSize * 1024 * 1024)
            {
                throw new UserFriendlyException(L(LKConstants.ExceedsTheMaximumSize, allowedMaxFileSize));
            }

            fileUnit = RollUniqueName(fileUnit);

            fileUnit.PhysicalPath = ConvertToRealPhysicalFilePath(fileUnit.Id);
            await using (Stream fileStream = new FileStream(fileUnit.PhysicalPath, FileMode.Create))
            {
                await input.File.CopyToAsync(fileStream);
            }

            fileUnit = await _repository.InsertAsync(fileUnit);

            return MapToEntityDto(fileUnit);
        }

        public async Task<FileUnitDto> RenameAsync(FileUnitRenameDto input)
        {
            if (input.Name.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException(L(LKConstants.YourDataIsInvalid));
            }

            NameValidation(input.Name);

            var fileUnit = await GetFileUnit(input.Id);
            var newFileName = Path.GetFileNameWithoutExtension(input.Name);
            var newFileExtension = Path.GetExtension(input.Name);
            if (newFileExtension.IsNullOrWhiteSpace())
            {
                fileUnit.Name = newFileName + fileUnit.Extension;
            }
            else
            {
                fileUnit.Name = input.Name;
                fileUnit.Extension = newFileExtension;
            }

            fileUnit.Description = input.Description;

            fileUnit.NameWithoutExtension = newFileName;
            fileUnit.FullName = StringHelper.TrueCombine(fileUnit.Directory, fileUnit.Name!);

            fileUnit = RollUniqueName(fileUnit);
            fileUnit = await _repository.UpdateAsync(fileUnit);
            if (fileUnit.IsFolder)
            {
                await UpdateChildDirectory(fileUnit);
            }

            return MapToEntityDto(fileUnit);
        }

        public async Task<FileUnitDto> MoveAsync(Guid id, Guid directoryId)
        {
            var fileUnit = await GetFileUnit(id);
            var directory = await GetFileUnit(directoryId);
            fileUnit.ParentId = directoryId;
            fileUnit.Directory = directory.FullName;
            fileUnit.FullName = StringHelper.TrueCombine(fileUnit.Directory, fileUnit.Name);
            fileUnit = RollUniqueName(fileUnit);
            fileUnit = await _repository.UpdateAsync(fileUnit);
            if (fileUnit.IsFolder)
            {
                await UpdateChildDirectory(fileUnit);
            }

            return MapToEntityDto(fileUnit);
        }

        public async Task DeleteAsync(Guid id)
        {
            var fileUnit = await GetAsync(id);
            if (fileUnit != null)
            {
                if (!fileUnit.IsFolder)
                {
                    File.Delete(fileUnit.PhysicalPath);
                }
                else
                {
                    var children = _repository.GetAll()
                    .Where(x => x.ParentId == fileUnit.Id)
                    .Select(x => x.Id)
                    .ToArray();
                    foreach (var itemId in children)
                    {
                        await DeleteAsync(itemId);
                    }
                }

                await _repository.DeleteAsync(id);
            }
        }

        [UnitOfWork]
        [AbpAuthorize]
        public async Task<FileUnitDto> GetUserDir(long? userId = null)
        {
            if (userId is null or <= 0)
            {
                userId = AbpSession.UserId;
            }

            var usersDir = await GetByFullName(UsersDirName) ??
                           await CreateDirectoryAsync(new CreateDirectoryDto
                           {
                               ParentId = null,
                               Name = UsersDirName,
                               Description =
                                   "Thư mục có trách nhiệm lưu media của toàn bộ người dùng trên hệ thống. Vui lòng không xóa."
                           });

            if (usersDir == null)
            {
                throw new UserFriendlyException(L(LKConstants.CanNotGetOrCreateDirectory_X));
            }

            await _repository.GetDbContext().SaveChangesAsync();

            var randomGuid = Guid.NewGuid().ToString().Replace("-", "");
            var name = $"{randomGuid}{userId}";
            var userIdStr = userId?.ToString() ?? "";
            var dir = CreateFilteredQuery(new PagedFileUnitResultRequestDto
            {
                IsFolder = true,
                Directory = usersDir.FullName
            }).Where(x => x.Name.Length == name.Length && x.Name.EndsWith(userIdStr))
              .ToList()
              .FirstOrDefault();

            var specifiedUserDir = MapToEntityDto(dir) ??
                                   await CreateDirectoryAsync(new CreateDirectoryDto
                                   {
                                       ParentId = usersDir.Id,
                                       Name = name,
                                       Description =
                                           $"Thư mục lưu trữ riêng cá nhân của người  dùng #{userId}"
                                   });
            if (specifiedUserDir == null)
            {
                throw new UserFriendlyException(L(LKConstants.CanNotGetOrCreateDirectory_X));
            }

            await _repository.GetDbContext().SaveChangesAsync();
            return specifiedUserDir;
        }

        [AbpAuthorize]
        public async Task<FileUnitDto> GetUserDirPicture(long? userId = null)
        {
            if (userId is null or <= 0)
            {
                userId = AbpSession.UserId;
            }

            var specifiedUserDir = await GetUserDir(userId);
            var userDirPicturesPath = StringHelper.TrueCombine(specifiedUserDir.FullName, "Pictures");

            var userDirPictures = await GetByFullName(userDirPicturesPath) ??
                                  await CreateDirectoryAsync(new CreateDirectoryDto
                                  {
                                      ParentId = specifiedUserDir.Id,
                                      Name = "Pictures",
                                      Description = "Lưu trữ hình ảnh"
                                  });
            if (userDirPictures == null)
            {
                throw new UserFriendlyException(L(LKConstants.CanNotGetOrCreateDirectory_X));
            }

            await _repository.GetDbContext().SaveChangesAsync();
            return userDirPictures;
        }

        #region Core Function
        private string ConvertToRealPhysicalFilePath(Guid id)
        {
            return StringHelper.TrueCombine(GetTodayContentPath, id.ToString());
        }

        private IQueryable<FileUnit> CreateFilteredQuery(PagedFileUnitResultRequestDto input)
        {
            var query = _repository.GetAll();
            query = query.WhereIf(input.IsFolder != null, x => x.IsFolder == input.IsFolder);
            query = query.WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
                x => x.Name.Contains(input.Keyword) || x.Description.Contains(input.Keyword));
            query = query.WhereIf(!input.Directory.IsNullOrWhiteSpace(),
                x => x.Directory.ToLower() == input.Directory.ToLower());
            query = query.WhereIf(input.Directory.IsNullOrWhiteSpace(),
                x => x.Directory == "/");
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

        private async Task<FileUnit> GetFileUnit(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new UserFriendlyException(L(LKConstants.YourDataIsInvalid));
            }

            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new UserFriendlyException(L(LKConstants.NotFoundYourData));
            }

            return entity;
        }

        /// <summary>
        /// Create suffix for name of file/directory if have one
        /// </summary>
        /// <returns></returns>
        private FileUnit RollUniqueName(FileUnit input, int count = 0)
        {
            var one = _repository.FirstOrDefault(x =>
                x.ParentId == input.ParentId &&
                x.IsFolder == input.IsFolder &&
                x.Name == input.Name && x.Id != input.Id);
            if (one == null)
            {
                if (count > 0)
                {
                    input.NameWithoutExtension = $"{input.NameWithoutExtension} ({count})";
                    input.FullName = StringHelper.TrueCombine(input.Directory, input.Name);
                }

                return input;
            }

            input.Name = $"{input.NameWithoutExtension} ({count + 1}){input.Extension}";
            return RollUniqueName(input, count + 1);
        }

        private void NameValidation(string name)
        {
            if (name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                throw new UserFriendlyException(L(LKConstants.NameOfFileOrFolderIsInvalid));
            }
        }

        private async Task UpdateChildDirectory(FileUnit parent)
        {
            if (!parent.IsFolder)
                return;
            var children = await _repository.GetAllListAsync(x => x.ParentId == parent.Id);
            foreach (var child in children)
            {
                if (child.IsFolder)
                {
                    await UpdateChildDirectory(child);
                }

                child.Directory = parent.FullName;
                child.FullName = StringHelper.TrueCombine(child.Directory, child.Name);
                await _repository.UpdateAsync(child);
            }
        }
        #endregion
    }
}

using Abp;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.Helpers;
using VinaCent.Blaze.Utilities;

namespace VinaCent.Blaze.AppCore.FileUnits
{
    public class FileUnitManager : AbpServiceBase, ITransientDependency
    {
        #region Runtime var
        /// <summary>
        /// Root folder for app file system
        /// </summary>
        /// <returns></returns>
        public string GetAppContentPath => StringHelper.TrueCombieAndEnsureDirExist(_webHostEnvironment.WebRootPath, FileUnitConsts.ContentPhysicalDirectory);

        /// <summary>
        /// Current content folder of current tenant
        /// </summary>
        /// <returns></returns>
        public string GetCurrentTenantContentPath => StringHelper.TrueCombieAndEnsureDirExist(GetAppContentPath, AbpSession.TenantId == null ? "root" : $"tenant_{AbpSession.TenantId}");

        /// <summary>
        /// Get content folder of current User
        /// </summary>
        public string GetCurrentUserContentPath => StringHelper.TrueCombieAndEnsureDirExist(GetCurrentTenantContentPath, AbpSession.UserId == null ? "anonymous" : $"user_{AbpSession.UserId}");

        /// <summary>
        /// Get today content folder
        /// </summary>
        public string GetTodayContentPath => StringHelper.TrueCombieAndEnsureDirExist(GetCurrentUserContentPath, DateTime.Now.ToString("yyyy/MM/dd"));
        #endregion

        private readonly IAbpSession AbpSession;
        private readonly IRepository<FileUnit, Guid> _repository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUnitManager(
            IAbpSession abpSession,
            IRepository<FileUnit, Guid> repository,
            IRepository<User, long> userRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            AbpSession = abpSession;
            _repository = repository;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;

        }

        /// <summary>
        /// Get file unit by given ID
        /// </summary>
        /// <param name="id">Real ID of virtual file unit</param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException">File unit not found!</exception>
        public async Task<FileUnit> GetById(Guid id)
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

        public async Task<FileUnit> GetByFullName(string fullName, bool removeIfFileNotExist = false)
        {
            if (fullName.IsNullOrWhiteSpace())
                return null;
            if (!fullName.StartsWith(StringHelper.FileSeparator))
                fullName = StringHelper.FileSeparator + fullName;
            var file = await _repository.FirstOrDefaultAsync(x => x.FullName == fullName);
            if (file == null)
                return null;

            // If is a folder => Return it
            if (file.IsFolder)
            {
                return file;
            }

            if (File.Exists(file.PhysicalPath))
            {
                return file;
            }

            // Check if file not found, also remove in virtual file manager
            // Remove in virtual file
            if (removeIfFileNotExist)
            {
                await _repository.DeleteAsync(file.Id);
            }

            return null;
        }

        public async Task<FileUnit> GetParentAsync(string directory)
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

            return currentDirectory;
        }

        public async Task<FileUnit> CreateDirectoryAsync(FileUnit input)
        {
            input.Id = Guid.NewGuid();

            NameValidation(input.Name);

            input.TenantId = AbpSession.TenantId;
            input.IsFolder = true;

            if (input.ParentId != null && input.ParentId != Guid.Empty)
            {
                var directory = await GetById(input.ParentId.Value);
                input.Directory = directory.FullName;
            }
            else
            {
                input.Directory = StringHelper.FileSeparator;
            }

            input.Name = input.Name;
            input.Extension = "";
            input.NameWithoutExtension = input.Name;
            input.FullName = StringHelper.TrueCombine(input.Directory, input.Name);
            input.Length = 0;

            input.PhysicalPath = "";

            input = RollUniqueName(input);
            return await _repository.InsertAsync(input);
        }

        public async Task<FileUnit> UploadFileAsync(FileUnit input, IFormFile file)
        {
            input.Id = Guid.NewGuid();

            if (file is not { Length: > 0 })
            {
                throw new UserFriendlyException(L(LKConstants.YouMustChooseAFileToUpload));
            }

            var allowedUploadFormats = (await SettingManager.GetSettingValueAsync(AppSettingNames.AllowedUploadFormats))
                ?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

            input.TenantId = AbpSession.TenantId;
            input.IsFolder = false;

            if (input.ParentId != null && input.ParentId != Guid.Empty)
            {
                var directory = await GetById(input.ParentId.Value);
                input.Directory = directory.FullName;
            }
            else
            {
                input.Directory = StringHelper.FileSeparator;
            }

            input.Name = Path.GetFileName(file.FileName);
            input.Extension = Path.GetExtension(file.FileName);

            MimeTypes.TryGetExtension(file.ContentType, out var realFileExtension);
            if (!realFileExtension.IsNullOrEmpty())
            {
                input.Extension = realFileExtension;
            }

            if (allowedUploadFormats == null || !allowedUploadFormats.Contains(input.Extension))
            {
                throw new UserFriendlyException(L(LKConstants.NotValidFormat));
            }

            input.NameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            input.FullName = StringHelper.TrueCombine(input.Directory, input.Name);
            input.Length = file.Length;

            if (input.Length > SettingManager.GetAllowMaxFileSizeInBytes())
            {
                throw new UserFriendlyException(L(LKConstants.ExceedsTheMaximumSize, SettingManager.GetAllowMaxFileSizeInMB()));
            }

            input = RollUniqueName(input);

            input.PhysicalPath = ConvertToRealPhysicalFilePath(input.Id);
            await using (Stream fileStream = new FileStream(input.PhysicalPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            if (!File.Exists(input.PhysicalPath))
            {
                throw new Exception("Save file fail!"); // TODO: Localize message
            }

            return await _repository.InsertAsync(input);
        }


        public async Task<FileUnit> RenameAsync(Guid id, string newName, string description)
        {
            if (newName.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException(L(LKConstants.YourDataIsInvalid));
            }

            NameValidation(newName);

            var fileUnit = await GetById(id);

            PreventStaticObjectChange(fileUnit.FullName, fileUnit.CreatorUserId);

            var newFileName = Path.GetFileNameWithoutExtension(newName);
            var newFileExtension = Path.GetExtension(newName);
            if (newFileExtension.IsNullOrWhiteSpace())
            {
                fileUnit.Name = newFileName + fileUnit.Extension;
            }
            else
            {
                fileUnit.Name = newName;
                fileUnit.Extension = newFileExtension;
            }

            fileUnit.Description = description;

            fileUnit.NameWithoutExtension = newFileName;
            fileUnit.FullName = StringHelper.TrueCombine(fileUnit.Directory, fileUnit.Name!);

            fileUnit = RollUniqueName(fileUnit);
            fileUnit = await _repository.UpdateAsync(fileUnit);
            if (fileUnit.IsFolder)
            {
                await UpdateChildDirectory(fileUnit);
            }

            return fileUnit;
        }

        public async Task<FileUnit> MoveAsync(Guid id, Guid directoryId)
        {
            var fileUnit = await GetById(id);

            PreventStaticObjectChange(fileUnit.FullName, fileUnit.CreatorUserId);

            var directory = await GetById(directoryId);
            fileUnit.ParentId = directoryId;
            fileUnit.Directory = directory.FullName;
            fileUnit.FullName = StringHelper.TrueCombine(fileUnit.Directory, fileUnit.Name);
            fileUnit = RollUniqueName(fileUnit);
            fileUnit = await _repository.UpdateAsync(fileUnit);
            if (fileUnit.IsFolder)
            {
                await UpdateChildDirectory(fileUnit);
            }

            return fileUnit;
        }

        public async Task DeleteAsync(Guid id)
        {
            var fileUnit = await GetById(id);

            PreventStaticObjectChange(fileUnit.FullName, fileUnit.CreatorUserId);

            if (!fileUnit.IsFolder)
            {
                try
                {
                    // Try to delete this file
                    File.Delete(fileUnit.PhysicalPath);
                }
                catch
                {
                    // Ignore process
                }
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

        [UnitOfWork]
        public async Task<FileUnit> GetUserDirAsync(long? userId = null)
        {
            if (userId is null or <= 0)
            {
                userId = AbpSession.UserId;
            }

            var usersDir = await GetByFullName(FileUnitConsts.UsersDirName) ??
                           await CreateDirectoryAsync(new FileUnit
                           {
                               ParentId = null,
                               Name = FileUnitConsts.UsersDirName,
                               Description =
                                   "Thư mục có trách nhiệm lưu media của toàn bộ người dùng trên hệ thống. Vui lòng không xóa."
                           });

            if (usersDir == null)
            {
                throw new UserFriendlyException(L(LKConstants.CanNotGetOrCreateDirectory_X));
            }

            await _repository.GetDbContext().SaveChangesAsync(); // TODO: Wrap with if check, run only when userDir have Id equals Guid.Empty

            var randomGuid = Guid.NewGuid().ToString().Replace("-", "");
            var name = $"{randomGuid}{userId}";
            var userIdStr = userId?.ToString() ?? "";

            var dir = await _repository.FirstOrDefaultAsync(x => x.IsFolder && x.Name.Substring(randomGuid.Length) == userIdStr && x.Directory.ToLower() == usersDir.FullName.ToLower());

            var specifiedUserDir = dir ?? await CreateDirectoryAsync(new FileUnit
            {
                ParentId = usersDir.Id,
                Name = name,
                Description = $"Thư mục lưu trữ riêng cá nhân của người  dùng #{userId}"
            });

            if (specifiedUserDir == null)
            {
                throw new UserFriendlyException(L(LKConstants.CanNotGetOrCreateDirectory_X));
            }

            await _repository.GetDbContext().SaveChangesAsync();
            return specifiedUserDir;
        }

        public async Task<FileUnit> GetUserSelfPictureDir()
        {
            var specifiedUserDir = await GetUserDirAsync(AbpSession.UserId);
            var userDirPicturesPath = StringHelper.TrueCombine(specifiedUserDir.FullName, "Pictures");

            var userDirPictures = await GetByFullName(userDirPicturesPath) ?? await CreateDirectoryAsync(new FileUnit
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



        private void PreventStaticObjectChange(string directory, long? creatorId)
        {
            // // If changer isn't creator => Deney
            if (AbpSession.UserId == creatorId) return;

            // If changer is creator => Permit
            directory = directory.EnsureStartsWith(StringHelper.FileSeparator.First());
            var isStatic = FileUnitConsts.StaticDirName.Any(x =>
                directory.EnsureStartsWith(StringHelper.FileSeparator.First())
                    .StartsWith(x.EnsureStartsWith(StringHelper.FileSeparator.First())));
            if (isStatic)
            {
                throw new UserFriendlyException(LKConstants.YouDoNotHaveToBeTheOwnerToPerformThisAction);
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


        private string ConvertToRealPhysicalFilePath(Guid id)
        {
            return StringHelper.TrueCombine(GetTodayContentPath, id.ToString());
        }
    }
}

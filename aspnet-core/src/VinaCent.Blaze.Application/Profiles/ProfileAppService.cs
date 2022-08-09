using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.CommonDatas;
using VinaCent.Blaze.AppCore.FileUnits;
using VinaCent.Blaze.AppCore.FileUnits.Dto;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.Helpers;
using VinaCent.Blaze.Profiles.Dto;

namespace VinaCent.Blaze.Profiles
{
    [AbpAuthorize]
    public class ProfileAppService : BlazeAppServiceBase, IProfileAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<CommonData, Guid> _repository;
        private readonly IFileUnitAppService _fileUnitAppService;

        public ProfileAppService(UserManager userManager,
            IRepository<CommonData, Guid> repository,
            IFileUnitAppService fileUnitAppService)
        {
            _userManager = userManager;
            _repository = repository;
            _fileUnitAppService = fileUnitAppService;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

        public async Task<ProfileDto> GetAsync()
        {
            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            return ObjectMapper.Map<ProfileDto>(user);
        }

        public async Task<ProfileDto> UpdateAsync(UpdateProfileDto input)
        {
            var user = await GetCurrentUserAsync();
            var commonDataQuery = _repository.GetAll();
            if(!await commonDataQuery.AnyAsync(x=>x.Type == "COUNTRY" && x.Key == input.Country)){
                input.Country = input.State = input.City = null;
            }
            else if(!await commonDataQuery.AnyAsync(x => x.Type == "STATE" && x.Key == input.State))
            {
                input.State = input.City = null;
            }
            else if(!await commonDataQuery.AnyAsync(x => x.Type == "CITY" && x.Key == input.City))
            {
                input.City = null;
            }
            input.Birthday = DateTime.Parse(input.BirthdayStr);
            user = ObjectMapper.Map(input,user);
            if (!input.ConcurrencyStamp.IsNullOrEmpty())
            {
                user.ConcurrencyStamp = input.ConcurrencyStamp;
            }

            if (!string.Equals(user.UserName, input.UserName, StringComparison.InvariantCultureIgnoreCase))
            {
                if (await SettingManager.IsTrueAsync(AppSettingNames.User.IsUserNameUpdateEnabled))
                {
                    (await UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();
                }
            }

            if (!string.Equals(user.EmailAddress, input.EmailAddress, StringComparison.InvariantCultureIgnoreCase))
            {
                if (await SettingManager.IsTrueAsync(AppSettingNames.User.IsEmailUpdateEnabled))
                {
                    (await UserManager.SetEmailAsync(user, input.EmailAddress)).CheckErrors();
                }
            }

            if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }

            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<ProfileDto>(user);
        }

        public async Task<ProfileDto> UpdateAvatar([FromForm] UpdateAvatarDto input)
        {
            var user = await GetCurrentUserAsync();
            if(user == null)
            {
                throw new UserFriendlyException(L(LKConstants.PleaseLogInBeforeDoThisAction));
            }

            var userDirPictures = await _fileUnitAppService.GetUserDirPicture(user.Id);

            var dto = new UploadFileUnitDto
            {
                ParentId = userDirPictures.Id,
                Description = "",
                File = input.File
            };
            var result = await _fileUnitAppService.UploadFileAsync(dto);
            if (result == null || result.Id == Guid.Empty || result.FullName.IsNullOrWhiteSpace())
                throw new UserFriendlyException(L("ChangeAvatarFail"));

            if (!user.Avatar.IsNullOrEmpty())
            {
                var avaFilePath = user.Avatar.ResourceFullName();
                var avaFile = await _fileUnitAppService.GetByFullName(avaFilePath);
                if (avaFile != null && avaFile.Id != Guid.Empty)
                    await _fileUnitAppService.DeleteAsync(avaFile.Id);
            }

            user.Avatar = result.ResourcePath;
            return ObjectMapper.Map<ProfileDto>(user);
        }
    }
}

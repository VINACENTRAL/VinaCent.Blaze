﻿using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.CommonDatas;
using VinaCent.Blaze.AppCore.FileUnits;
using VinaCent.Blaze.AppCore.FileUnits.Dto;
using VinaCent.Blaze.AppCore.TextTemplates;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.Helpers;
using VinaCent.Blaze.Helpers.Encryptions;
using VinaCent.Blaze.Profiles.Dto;

namespace VinaCent.Blaze.Profiles
{
    [AbpAuthorize]
    public class ProfileAppService : BlazeAppServiceBase, IProfileAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<CommonData, Guid> _repository;
        private readonly IFileUnitAppService _fileUnitAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAESHelper _aesHelper;
        private readonly ITextTemplateAppService _textTemplateAppService;
        private readonly IEmailSender _emailSender;

        public ProfileAppService(UserManager userManager,
            IRepository<CommonData, Guid> repository,
            IFileUnitAppService fileUnitAppService,
            IHttpContextAccessor httpContextAccessor,
            ITextTemplateAppService textTemplateAppService,
            IEmailSender emailSender,
            IAESHelper aesHelper)
        {
            _userManager = userManager;
            _repository = repository;
            _fileUnitAppService = fileUnitAppService;
            _httpContextAccessor = httpContextAccessor;
            _textTemplateAppService = textTemplateAppService;
            _emailSender = emailSender;
            _aesHelper = aesHelper;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await GetCurrentUserAsync();

            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
                await UserManager.UpdateSecurityStampAsync(user);
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
                throw new UserFriendlyException(L(LKConstants.ChangeAvatarFail));

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

        public async Task<string> SendConfirmCodeAsync(RequestEmailDto input)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                throw new UserFriendlyException(L(LKConstants.UnknownHttpContextRequest));
            }

            var user = await GetCurrentUserAsync();

            var template = await _textTemplateAppService.GetSecurityCodeAsync();

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, user.EmailAddress);

            // Random keycode
            var key = Guid.NewGuid().ToString("N")[..6].ToUpper();

            // For security double
            var verify = _aesHelper.Encrypt($"{user.UserName}|{token}", key);

            var systemName = await SettingManager.GetSettingValueAsync(AppSettingNames.SiteName);

            var subject = $"[{systemName.ToUpper()}] {L(template.Name)}";

            await _emailSender.SendAsync(user.EmailAddress, subject, template.Apply(key));

            return verify;
        }
        public async Task<string> ConfirmCodeAsync(ConfirmCodeDto input)
        {
            string verify;
            try
            {
                verify = _aesHelper.Decrypt(input.Token, input.ConfirmCode);
            } 
            catch
            {
                throw new UserFriendlyException(L(LKConstants.InvalidCode));
            }
            var raw = verify.Split("|");
            var userName = raw.FirstOrDefault();
            var currentUser = await GetCurrentUserAsync();
            var token = raw.LastOrDefault();
            if (currentUser.UserName == userName)
            {
                return token;
            }

            throw new UserFriendlyException(L(LKConstants.InvalidCode));
        }

        public async Task<bool> ChangeEmailAsync(ChangeEmailDto input)
        {
            string verify;
            try
            {
                verify = _aesHelper.Decrypt(input.VerifyToken, input.ConfirmCode);
            }
            catch
            {
                throw new UserFriendlyException(L(LKConstants.InvalidCode));
            }
            var raw = verify.Split("|");
            var userName = raw.FirstOrDefault();
            var currentUser = await GetCurrentUserAsync();
            if (currentUser.UserName == userName)
            {
                if (!string.Equals(currentUser.EmailAddress, input.NewEmail, StringComparison.InvariantCultureIgnoreCase))
                {
                    (await UserManager.SetEmailAsync(currentUser, input.NewEmail)).CheckErrors();
                    return true;
                }
                return false;
            }

            throw new UserFriendlyException(L(LKConstants.InvalidCode));
        }
    }
}

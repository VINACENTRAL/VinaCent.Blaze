using Abp.Configuration;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.Profiles.Dto;

namespace VinaCent.Blaze.Profiles
{
    public class ProfileAppService : BlazeAppServiceBase, IProfileAppService
    {
        private readonly UserManager _userManager;

        public ProfileAppService(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
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
            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
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

            user.Name = input.Name;
            user.Surname = input.Surname;
            user.City = input.City;
            user.Country = input.Country;
            user.ZipCode = input.ZipCode;
            user.Description = input.Description;

            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<ProfileDto>(user);
        }
    }
}

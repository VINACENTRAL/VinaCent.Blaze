using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using VinaCent.Blaze.Authorization.Roles;
using VinaCent.Blaze.Common;

namespace VinaCent.Blaze.Authorization.Users
{
    //https://github.com/aspnetboilerplate/aspnetboilerplate/blob/7647670db2a42b25be864094a42afe03ff118c2d/src/Abp.ZeroCore/Authorization/Users/AbpUserManager.cs 
    public class UserManager : AbpUserManager<Role, User>
    {
        public UserManager(
          RoleManager roleManager,
          UserStore store,
          IOptions<IdentityOptions> optionsAccessor,
          IPasswordHasher<User> passwordHasher,
          IEnumerable<IUserValidator<User>> userValidators,
          IEnumerable<IPasswordValidator<User>> passwordValidators,
          ILookupNormalizer keyNormalizer,
          IdentityErrorDescriber errors,
          IServiceProvider services,
          ILogger<UserManager<User>> logger,
          IPermissionManager permissionManager,
          IUnitOfWorkManager unitOfWorkManager,
          ICacheManager cacheManager,
          IRepository<OrganizationUnit, long> organizationUnitRepository,
          IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
          IOrganizationUnitSettings organizationUnitSettings,
          ISettingManager settingManager,
          IRepository<UserLogin, long> userLoginRepository)
          : base(
              roleManager,
              store,
              optionsAccessor,
              passwordHasher,
              userValidators,
              passwordValidators,
              keyNormalizer,
              errors,
              services,
              logger,
              permissionManager,
              unitOfWorkManager,
              cacheManager,
              organizationUnitRepository,
              userOrganizationUnitRepository,
              organizationUnitSettings,
              settingManager,
              userLoginRepository)
        {
            // Sign Options
            Options.SignIn.RequireConfirmedEmail = GlobalConstants.SignInOptions.RequireConfirmedEmail;
            Options.SignIn.RequireConfirmedPhoneNumber = GlobalConstants.SignInOptions.RequireConfirmedPhoneNumber;
            Options.SignIn.RequireConfirmedAccount = GlobalConstants.SignInOptions.RequireConfirmedAccount;

            // UserOptions
            Options.User.AllowedUserNameCharacters = GlobalConstants.UserOptions.AllowedUserNameCharacters;
            Options.User.RequireUniqueEmail = GlobalConstants.UserOptions.RequireUniqueEmail;
        }
    }
}

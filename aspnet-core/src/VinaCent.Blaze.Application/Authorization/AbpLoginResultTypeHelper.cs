using System;
using Abp;
using Abp.Authorization;
using Abp.Dependency;
using Abp.UI;

namespace VinaCent.Blaze.Authorization
{
    public class AbpLoginResultTypeHelper : AbpServiceBase, ITransientDependency
    {
        public AbpLoginResultTypeHelper()
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;
        }

        public Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L(LKConstants.LoginFailed), L(LKConstants.InvalidUserNameOrPassword));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L(LKConstants.LoginFailed), L(LKConstants.ThereIsNoTenantDefinedWithName_X, tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L(LKConstants.LoginFailed), L(LKConstants.TenantIsNotActive, tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L(LKConstants.LoginFailed), L(LKConstants.UserIsNotActiveAndCanNotLogin, usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L(LKConstants.LoginFailed), L(LKConstants.UserEmailIsNotConfirmedAndCanNotLogin));
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L(LKConstants.LoginFailed), L(LKConstants.UserLockedOutMessage));
                default: // Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L(LKConstants.LoginFailed));
            }
        }

        public string CreateLocalizedMessageForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    throw new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return L(LKConstants.InvalidUserNameOrPassword);
                case AbpLoginResultType.InvalidTenancyName:
                    return L(LKConstants.ThereIsNoTenantDefinedWithName_X, tenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    return L(LKConstants.TenantIsNotActive, tenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    return L(LKConstants.UserIsNotActiveAndCanNotLogin, usernameOrEmailAddress);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return L(LKConstants.UserEmailIsNotConfirmedAndCanNotLogin);
                default: // Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return L(LKConstants.LoginFailed);
            }
        }
    }
}

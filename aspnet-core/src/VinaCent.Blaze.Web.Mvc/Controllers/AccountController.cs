using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Notifications;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Models;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Authorization;
using VinaCent.Blaze.Controllers;
using VinaCent.Blaze.Identity;
using VinaCent.Blaze.MultiTenancy;
using VinaCent.Blaze.Sessions;
using VinaCent.Blaze.Web.Models.Account;
using VinaCent.Blaze.Web.Views.Shared.Components.TenantChange;
using Microsoft.AspNetCore.Http;
using VinaCent.Blaze.Web.Common;
using VinaCent.Blaze.Sessions.Dto;
using VinaCent.Blaze.Helpers.Encryptions;
using VinaCent.Blaze.Configuration;
using Abp.Net.Mail;
using VinaCent.Blaze.AppCore.TextTemplates;
using VinaCent.Blaze.Utilities;
using VinaCent.Blaze.Authorization.Accounts.Dto;
using System.Web;

namespace VinaCent.Blaze.Web.Controllers
{
    [Route("account")]
    public class AccountController : BlazeControllerBase
    {
        private readonly UserManager _userManager;

        private readonly TenantManager _tenantManager;

        private readonly IMultiTenancyConfig _multiTenancyConfig;

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;

        private readonly LogInManager _logInManager;

        private readonly SignInManager _signInManager;

        private readonly UserRegistrationManager _userRegistrationManager;

        private readonly ISessionAppService _sessionAppService;

        private readonly ITenantCache _tenantCache;

        private readonly INotificationPublisher _notificationPublisher;

        private readonly IAESHelper _aesHelper;

        private readonly ITextTemplateAppService _textTemplateAppService;

        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            TenantManager tenantManager,
            IUnitOfWorkManager unitOfWorkManager,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            LogInManager logInManager,
            SignInManager signInManager,
            UserRegistrationManager userRegistrationManager,
            ISessionAppService sessionAppService,
            ITenantCache tenantCache,
            INotificationPublisher notificationPublisher,
            IAESHelper aesHelper,
            ITextTemplateAppService textTemplateAppService,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
            _tenantManager = tenantManager;
            _unitOfWorkManager = unitOfWorkManager;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _logInManager = logInManager;
            _signInManager = signInManager;
            _userRegistrationManager = userRegistrationManager;
            _sessionAppService = sessionAppService;
            _tenantCache = tenantCache;
            _notificationPublisher = notificationPublisher;
            _aesHelper = aesHelper;
            _textTemplateAppService = textTemplateAppService;
            _emailSender = emailSender;
        }


        #region Login / Logout

        [HttpGet("login")]
        public async Task<ActionResult>
        Login(
            string userNameOrEmailAddress = "",
            string returnUrl = "",
            string successMessage = ""
        )
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAppHome();
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = GetAppHomeUrl();
            }

            var model = new LoginFormViewModel
            {
                UsernameOrEmailAddress = GetLoggedInUserName(userNameOrEmailAddress),
                ReturnUrl = returnUrl,
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                IsSelfRegistrationAllowed = IsSelfRegistrationEnabled(),
                MultiTenancySide = AbpSession.MultiTenancySide
            };

            if (!string.IsNullOrWhiteSpace(model.UsernameOrEmailAddress))
            {
                var previousLoggedUser = await _userManager.FindByNameOrEmailAsync(model.UsernameOrEmailAddress);
                if (previousLoggedUser != null)
                {
                    ViewBag.UserLoginInfo = ObjectMapper.Map<UserLoginInfoDto>(previousLoggedUser);
                } else
                {
                    model.UsernameOrEmailAddress = string.Empty;
                    SetOrRemoveLoggedInUserName(); // Remove old value
                }
            }

            return View(model);
        }

        [HttpPost("login")]
        [UnitOfWork]
        public async Task<JsonResult>
        Login(
            LoginViewModel loginModel,
            string returnUrl = "",
            string returnUrlHash = ""
        )
        {
            returnUrl = NormalizeReturnUrl(returnUrl);
            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl += returnUrlHash;
            }

            var loginResult =
                await GetLoginResultAsync(loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                GetTenancyNameOrNull());

            await _signInManager
                .SignInAsync(loginResult.Identity, loginModel.RememberMe);
            await UnitOfWorkManager.Current.SaveChangesAsync();

            // Only user select remember me can store their user name
            if (loginModel.RememberMe)
            {
                SetOrRemoveLoggedInUserName(loginModel.UsernameOrEmailAddress);
            }
            AddSuccessNotify(L(LKConstants.LoginSuccessMessage));
            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            SetOrRemoveLoggedInUserName();
            if (await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.AppSys_DoNotShowLogoutScreen))
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpGet("lockout")]
        public async Task<ActionResult> Lockout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet("lockout/change")]
        public ActionResult LockoutChangeAccount(
            string userNameOrEmailAddress = "",
            string returnUrl = "",
            string successMessage = "")
        {
            SetOrRemoveLoggedInUserName();
            return RedirectToAction("Login", new
            {
                userNameOrEmailAddress,
                returnUrl,
                successMessage
            });
        }

        private async Task<AbpLoginResult<Tenant, User>>
        GetLoginResultAsync(
            string usernameOrEmailAddress,
            string password,
            string tenancyName
        )
        {
            var loginResult =
                await _logInManager
                    .LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper
                        .CreateExceptionForFailedLoginAttempt(loginResult
                            .Result,
                        usernameOrEmailAddress,
                        tenancyName);
            }
        }


        #endregion



        #region Register

        [HttpGet("register")]
        public ActionResult Register()
        {
            return User.Identity?.IsAuthenticated == true ? RedirectToAppHome() : RegisterView(new RegisterViewModel());
        }

        private ActionResult RegisterView(RegisterViewModel model)
        {
            if (!IsSelfRegistrationEnabled()) return NotFound();
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;

            return View("Register", model);
        }

        private bool IsSelfRegistrationEnabled()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return false; // No registration enabled for host users!
            }

            return SettingManager.GetSettingValue<bool>(AppSettingNames.AppSys_IsRegisterEnabled);
        }

        [HttpPost("register")]
        [UnitOfWork]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!IsSelfRegistrationEnabled()) return NotFound();

            try
            {
                ExternalLoginInfo externalLoginInfo = null;
                if (model.IsExternalLogin)
                {
                    externalLoginInfo =
                        await _signInManager.GetExternalLoginInfoAsync();
                    if (externalLoginInfo == null)
                    {
                        throw new Exception("Can not external login!");
                    }

                    model.UserName = model.EmailAddress;
                    model.Password =
                        Authorization.Users.User.CreateRandomPassword();
                }
                else
                {
                    if (
                        model.UserName.IsNullOrEmpty() ||
                        model.Password.IsNullOrEmpty()
                    )
                    {
                        throw new UserFriendlyException(L(LKConstants
                                .FormIsNotValidMessage));
                    }
                }

                var user =
                    await _userRegistrationManager
                        .RegisterAsync(model.Name,
                        model.Surname,
                        model.EmailAddress,
                        model.UserName,
                        model.Password,
                        true); // Assumed email address is always confirmed. Change this if you want to implement email confirmation.

                // Getting tenant-specific settings
                var isEmailConfirmationRequiredForLogin =
                    await SettingManager
                        .GetSettingValueAsync<bool>(AbpZeroSettingNames
                            .UserManagement
                            .IsEmailConfirmationRequiredForLogin);

                if (model.IsExternalLogin)
                {
                    Debug.Assert(externalLoginInfo != null);

                    if (
                        string
                            .Equals(externalLoginInfo
                                .Principal
                                .FindFirstValue(ClaimTypes.Email),
                            model.EmailAddress,
                            StringComparison.OrdinalIgnoreCase)
                    )
                    {
                        user.IsEmailConfirmed = true;
                    }

                    user.Logins =
                        new List<UserLogin> {
                            new UserLogin {
                                LoginProvider = externalLoginInfo.LoginProvider,
                                ProviderKey = externalLoginInfo.ProviderKey,
                                TenantId = user.TenantId
                            }
                        };
                }

                await _unitOfWorkManager.Current.SaveChangesAsync();

                Debug.Assert(user.TenantId != null);

                var tenant =
                    await _tenantManager.GetByIdAsync(user.TenantId.Value);

                // Directly login if possible
                if (
                    user.IsActive &&
                    (
                    user.IsEmailConfirmed ||
                    !isEmailConfirmationRequiredForLogin
                    )
                )
                {
                    AbpLoginResult<Tenant, User> loginResult;
                    if (externalLoginInfo != null)
                    {
                        loginResult =
                            await _logInManager
                                .LoginAsync(externalLoginInfo,
                                tenant.TenancyName);
                    }
                    else
                    {
                        loginResult =
                            await GetLoginResultAsync(user.UserName,
                            model.Password,
                            tenant.TenancyName);
                    }

                    if (loginResult.Result == AbpLoginResultType.Success)
                    {
                        await _signInManager
                            .SignInAsync(loginResult.Identity, false);
                        return Redirect(GetAppHomeUrl());
                    }

                    Logger
                        .Warn("New registered user could not be login. This should not be normally. login result: " +
                        loginResult.Result);
                }

                return View("RegisterResult",
                new RegisterResultViewModel
                {
                    TenancyName = tenant.TenancyName,
                    NameAndSurname = user.Name + " " + user.Surname,
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    IsEmailConfirmed = user.IsEmailConfirmed,
                    IsActive = user.IsActive,
                    IsEmailConfirmationRequiredForLogin =
                        isEmailConfirmationRequiredForLogin
                });
            }
            catch (UserFriendlyException ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Register", model);
            }
        }


        #endregion



        #region Reset Password
        [HttpGet("reset-password")]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost("reset-password")]
        [UnitOfWork]
        public async Task<JsonResult> RequestResetPassword(ResetPasswordRequestInput input)
        {
            var user = await _userManager.FindByEmailAsync(input.EmailAddress);
            if (user == null)
            {
                throw new UserFriendlyException(LKConstants.NoAccountsHaveBeenRegisteredWithThisEmailYet);
            }

            var template = await _textTemplateAppService.GetPasswordResetTemplateAsync();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Random keycode
            var key = Guid.NewGuid().ToString("N")[..4].ToUpper();

            // For security double
            var verify = _aesHelper.Encrypt($"{user.UserName}|{token}", key);

            verify = HttpUtility.UrlEncode(verify);

            var path = HttpContext.Request.GetCurrentHost() + Url.Action(nameof(VerifyAndChangePassword), "Account", new { verify, key, email = user.EmailAddress });

            var pathWithoutKey = HttpContext.Request.GetCurrentHost() + Url.Action(nameof(VerifyAndChangePassword), "Account", new { verify, email = user.EmailAddress });

            var systemName = await SettingManager.GetSettingValueAsync(AppSettingNames.SiteName);

            var subject = $"[{systemName.ToUpper()}] {L(template.Name)}";

            await _emailSender.SendAsync(user.EmailAddress, subject, template.Apply(path, key));

            return Json(new AjaxResponse { TargetUrl = pathWithoutKey });
        }

        [HttpGet("reset-password/verify")]
        [UnitOfWork]
        public async Task<IActionResult> VerifyAndChangePassword([FromQuery] string verify, [FromQuery] string key, [FromQuery] string email)
        {
            ViewBag.Email = email;
            ViewBag.Verify = verify;

            if (string.IsNullOrEmpty(key))
            {
                if (email.IsNullOrEmpty())
                {
                    return NotFound();
                }

                return View("~/Views/Account/ResetPassword/VerifyKeyCode.cshtml");
            }

            string userName, token;
            try
            {
                verify = HttpUtility.UrlDecode(verify);
                verify = _aesHelper.Decrypt(verify, key);
                var raw = verify.Split("|");
                userName = raw.FirstOrDefault();
                token = raw.LastOrDefault();
            }
            catch (Exception)
            {
                ViewBag.Status = false;
                ViewBag.Message = L(LKConstants.VerificationFailed);
                return View("~/Views/Account/ResetPassword/VerifyStatus.cshtml");
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                ViewBag.Status = false;
                ViewBag.Message = L(LKConstants.UserDoesNotExistOnTheSystem);
                return View("~/Views/Account/ResetPassword/VerifyStatus.cshtml");
            }

            var model = new ResetPasswordInput
            {
                EmailAddress = user.EmailAddress,
                Token = token
            };

            return View(model);
        }

        #endregion



        #region External Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl =
                Url
                    .Action("ExternalLoginCallback",
                    "Account",
                    new { ReturnUrl = returnUrl });

            return Challenge(// TODO: ...?
            // new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties
            // {
            //     Items = { { "LoginProvider", provider } },
            //     RedirectUri = redirectUrl
            // },
            provider);
        }

        [UnitOfWork]
        public async Task<ActionResult>
        ExternalLoginCallback(string returnUrl, string remoteError = null)
        {
            returnUrl = NormalizeReturnUrl(returnUrl);

            if (remoteError != null)
            {
                Logger
                    .Error("Remote Error in ExternalLoginCallback: " +
                    remoteError);
                throw new UserFriendlyException(L(LKConstants
                        .CouldNotCompleteLoginOperation));
            }

            var externalLoginInfo =
                await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                Logger.Warn("Could not get information from external login.");
                return RedirectToAction(nameof(Login));
            }

            await _signInManager.SignOutAsync();

            var tenancyName = GetTenancyNameOrNull();

            var loginResult =
                await _logInManager.LoginAsync(externalLoginInfo, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    await _signInManager
                        .SignInAsync(loginResult.Identity, false);
                    return Redirect(returnUrl);
                case AbpLoginResultType.UnknownExternalLogin:
                    return await RegisterForExternalLogin(externalLoginInfo);
                default:
                    throw _abpLoginResultTypeHelper
                        .CreateExceptionForFailedLoginAttempt(loginResult
                            .Result,
                        externalLoginInfo
                            .Principal
                            .FindFirstValue(ClaimTypes.Email)
                            ?? externalLoginInfo.ProviderKey,
                        tenancyName);
            }
        }

        private async Task<ActionResult>
        RegisterForExternalLogin(ExternalLoginInfo externalLoginInfo)
        {
            var email =
                externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            var nameinfo =
                ExternalLoginInfoHelper
                    .GetNameAndSurnameFromClaims(externalLoginInfo
                        .Principal
                        .Claims
                        .ToList());

            var viewModel =
                new RegisterViewModel
                {
                    EmailAddress = email,
                    Name = nameinfo.name,
                    Surname = nameinfo.surname,
                    IsExternalLogin = true,
                    ExternalLoginAuthSchema = externalLoginInfo.LoginProvider
                };

            if (
                nameinfo.name != null &&
                nameinfo.surname != null &&
                email != null
            )
            {
                return await Register(viewModel);
            }

            return RegisterView(viewModel);
        }

        [UnitOfWork]
        protected async Task<List<Tenant>>
        FindPossibleTenantsOfUserAsync(UserLoginInfo login)
        {
            List<User> allUsers;
            using (
                _unitOfWorkManager
                    .Current
                    .DisableFilter(AbpDataFilters.MayHaveTenant)
            )
            {
                allUsers = await _userManager.FindAllAsync(login);
            }

            return allUsers
                .Where(u => u.TenantId != null)
                .Select(u =>
                    AsyncHelper
                        .RunSync(() =>
                            _tenantManager.FindByIdAsync(u.TenantId.Value)))
                .ToList();
        }


        #endregion



        #region Helpers

        public ActionResult RedirectToAppHome()
        {
            return RedirectToAction("Index", "Home");
        }

        public string GetAppHomeUrl()
        {
            return Url.Action("Index", "Home");
        }


        #endregion



        #region Change Tenant
        [HttpPost("tenant-change-modal")]
        public async Task<ActionResult> TenantChangeModal()
        {
            var loginInfo =
                await _sessionAppService.GetCurrentLoginInformations();
            return View("/Views/Shared/Components/TenantChange/_ChangeModal.cshtml",
            new ChangeModalViewModel
            {
                TenancyName = loginInfo.Tenant?.TenancyName
            });
        }


        #endregion



        #region Common

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache
                .GetOrNull(AbpSession.TenantId.Value)?
                .TenancyName;
        }

        private string
        NormalizeReturnUrl(
            string returnUrl,
            Func<string> defaultValueBuilder = null
        )
        {
            defaultValueBuilder ??= GetAppHomeUrl;

            if (returnUrl.IsNullOrEmpty())
            {
                return defaultValueBuilder();
            }

            return Url.IsLocalUrl(returnUrl) ? returnUrl : defaultValueBuilder();
        }


        #endregion


        #region Etc

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expireDay"></param>
        /// <returns></returns>
        //[AbpMvcAuthorize]
        //[HttpGet("test-notify")]
        //public async Task<ActionResult> TestNotification(string message = "")
        //{
        //    if (message.IsNullOrEmpty())
        //    {
        //        message = "This is a test notification, created at " + Clock.Now;
        //    }

        //    var defaultTenantAdmin = new UserIdentifier(1, 2);
        //    var hostAdmin = new UserIdentifier(null, 1);

        //    await _notificationPublisher.PublishAsync(
        //            "App.SimpleMessage",
        //            new MessageNotificationData(message),
        //            severity: NotificationSeverity.Info,
        //            userIds: new[] { defaultTenantAdmin, hostAdmin }
        //         );

        //    return Content("Sent notifßication: " + message);
        //}

        private void SetOrRemoveLoggedInUserName(string value = "", int expireDay = 5 * 365) // Default is 5 years
        {
            var key = $"{BlazeWebConstants.PreviousAccount}${AbpSession.TenantId ?? 0}";
            // Remove old
            Response.Cookies.Delete(key);

            // Add/Update newer
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(expireDay),
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            value = _aesHelper.Encrypt(value);

            Response.Cookies.Append(key, value, option);
        }

        private string GetLoggedInUserName(string defaultEmailAddress)
        {
            if (!defaultEmailAddress.IsNullOrWhiteSpace())
            {
                return defaultEmailAddress;
            }

            var key = $"{BlazeWebConstants.PreviousAccount}${AbpSession.TenantId ?? 0}";
            var value = Request.Cookies[key] ?? "";

            if (value.IsNullOrEmpty()) return value;

            try
            {
                value = _aesHelper.Decrypt(value);
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return value;
        }

        #endregion
    }
}

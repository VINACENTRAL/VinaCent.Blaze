using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.TextTemplates.Dto;
using VinaCent.Blaze.Authorization.Users;
using VinaCent.Blaze.Configuration;
using VinaCent.Blaze.MultiTenancy;
using VinaCent.Blaze.Users.Dto;

namespace VinaCent.Blaze.AppCore.TextTemplates
{
    public class TextTemplateAppService : AsyncCrudAppService<TextTemplate, TextTemplateDto, Guid, PagedTextTemplateResultRequestDto, CreateTextTemplateDto, UpdateTextTemplateDto>,
        ITextTemplateAppService
    {
        private readonly UserManager _userManager;
        private readonly TenantManager _tenantManager;
        private readonly IEmailSender _emailSender;

        public TextTemplateAppService(IRepository<TextTemplate, Guid> repository, UserManager userManager, TenantManager tenantManager, IEmailSender emailSender) : base(repository)
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;

            _userManager = userManager;
            _tenantManager = tenantManager;
            _emailSender = emailSender;
        }

        public async Task<PagedResultDto<TextTemplateListDto>> GetAllListAsync(PagedTextTemplateResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);
            query = ApplySorting(query, input);
            var total = await query.CountAsync();
            query = ApplyPaging(query, input);
            var auditLogs = await query.ToListAsync();
            var dtoList = ObjectMapper.Map<List<TextTemplateListDto>>(auditLogs);
            return new PagedResultDto<TextTemplateListDto>(total, dtoList);
        }

        public async Task TestTextTemplateAsync(TestTextTemplateDto input)
        {
            var textTemplate = await Repository.GetAsync(input.TextTemplateId);
            if (textTemplate == null)
            {
                throw new UserFriendlyException("Text template was not found!");
            }

            var ttDto = ObjectMapper.Map<TextTemplateDto>(textTemplate);

            var systemName = await SettingManager.GetSettingValueAsync(AppSettingNames.SiteName);

            var currentUser = await GetCurrentUserAsync();
            var currentUserDto = ObjectMapper.Map<UserDto>(currentUser);

            var subject = $"[{systemName}] Test Text template of \"{L(textTemplate.Name)}\" by [";
            if (AbpSession.TenantId != null)
            {
                var currentTenant = await GetCurrentTenantAsync();
                subject += $"{currentTenant.TenancyName}/";
            }
            else
            {
                subject += $"{currentUserDto.UserName}]";
            }


            var mailMsg = new MailMessage
            {
                To = { input.Receiver },
                Subject = subject,
                Body = ttDto.Apply(input.Parameters),
                IsBodyHtml = true
            };

            //Send a notification email
            await _emailSender.SendAsync(mailMsg);
        }

        protected async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected Task<Tenant> GetCurrentTenantAsync()
        {
            return _tenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        public Task<TextTemplateDto> GetPasswordResetTemplateAsync()
        {
            return GetByNameAsync(TextTemplate.PasswordReset.Name);
        }

        public Task<TextTemplateDto> GetEmailConfirmationAsync()
        {
            return GetByNameAsync(TextTemplate.EmailConfirmation.Name);
        }

        public Task<TextTemplateDto> GetSecurityCodeAsync()
        {
            return GetByNameAsync(TextTemplate.SecurityCode.Name);
        }

        public Task<TextTemplateDto> GetWelcomeAfterJoinSystemAsync()
        {
            return GetByNameAsync(TextTemplate.WelcomeAfterJoinSystem.Name);
        }

        public async Task<TextTemplateDto> GetByNameAsync(string name)
        {
            var template = await Repository.FirstOrDefaultAsync(x => x.Name == name);
            if (template == null && TextTemplate.Defaults.Any(x => x.Name == name))
                template = TextTemplate.Defaults.FirstOrDefault(x => x.Name == name);

            return MapToEntityDto(template);
        }
    }
}

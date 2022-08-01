using Abp.Configuration;
using Abp.Net.Mail;
using System.Threading.Tasks;
using VinaCent.Blaze.Web.Areas.AdminCP.Models.EmailConfiguration.SetUpMailServer;

namespace VinaCent.Blaze.AppCore.Emailer
{
    public class EmailerAppService : BlazeAppServiceBase, IEmailerAppService
    {

        public async Task<SetupEmailerDto> GetSetupAsync()
        {
            var model = new SetupEmailerDto
            {
                DefaultFromAddress = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromAddress),
                DefaultFromDisplayName = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromDisplayName),
                SmtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host),
                SmtpPort = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Port),
                SmtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName),
                SmtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password),
                SmtpDomain = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Domain),
                SmtpEnableSsl = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.EnableSsl),
                SmtpUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials),
            };

            return model;
        }

        public async Task SaveSetupAsync(SetupEmailerDto input)
        {
            if (AbpSession.TenantId == null)
            {
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Host, input.SmtpHost);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Port, input.SmtpPort);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UserName, input.SmtpUserName);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Password, input.SmtpPassword);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Domain, input.SmtpDomain);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.EnableSsl, input.SmtpEnableSsl.ToString().ToLower());
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UseDefaultCredentials, input.SmtpUseDefaultCredentials.ToString().ToLower());
            }
            else
            {
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.Value, EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.Value, EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.Value, EmailSettingNames.Smtp.Host, input.SmtpHost);
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.Value, EmailSettingNames.Smtp.Port, input.SmtpPort);
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.Value, EmailSettingNames.Smtp.UserName, input.SmtpUserName);
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.Value, EmailSettingNames.Smtp.Password, input.SmtpPassword);
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.Value, EmailSettingNames.Smtp.Domain, input.SmtpDomain);
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.Value, EmailSettingNames.Smtp.EnableSsl, input.SmtpEnableSsl.ToString().ToLower());
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.TenantId.Value, EmailSettingNames.Smtp.UseDefaultCredentials, input.SmtpUseDefaultCredentials.ToString().ToLower());
            }
        }
    }
}

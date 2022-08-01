using Abp.Application.Services;
using System.Threading.Tasks;
using VinaCent.Blaze.Web.Areas.AdminCP.Models.EmailConfiguration.SetUpMailServer;

namespace VinaCent.Blaze.AppCore.Emailer
{
    public interface IEmailerAppService : IApplicationService
    {
        Task<SetupEmailerDto> GetSetupAsync();
        Task SaveSetupAsync(SetupEmailerDto input);
        Task TestEmailSenderAsync(TestEmailSenderDto input);
    }
}

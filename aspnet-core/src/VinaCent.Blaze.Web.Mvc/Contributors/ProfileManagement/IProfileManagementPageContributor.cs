using System.Threading.Tasks;

namespace VinaCent.Blaze.Web.Contributors.ProfileManagement
{
    public interface IProfileManagementPageContributor
    {
        Task ConfigureAsync(ProfileManagementPageCreationContext context);
    }
}

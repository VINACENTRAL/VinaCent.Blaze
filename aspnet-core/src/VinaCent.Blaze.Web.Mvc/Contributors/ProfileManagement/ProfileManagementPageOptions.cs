using System.Collections.Generic;

namespace VinaCent.Blaze.Web.Contributors.ProfileManagement
{
    public class ProfileManagementPageOptions
    {
        public List<IProfileManagementPageContributor> Contributors { get; }

        public ProfileManagementPageOptions()
        {
            Contributors = new List<IProfileManagementPageContributor>();
        }
    }
}

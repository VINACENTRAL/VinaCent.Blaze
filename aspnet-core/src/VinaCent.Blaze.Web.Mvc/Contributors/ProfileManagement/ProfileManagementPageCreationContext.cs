using System;
using System.Collections.Generic;

namespace VinaCent.Blaze.Web.Contributors.ProfileManagement
{
    public class ProfileManagementPageCreationContext
    {
        public IServiceProvider ServiceProvider { get; }

        public List<ProfileManagementPageGroup> Groups { get; }

        public ProfileManagementPageCreationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            Groups = new List<ProfileManagementPageGroup>();
        }
    }
}

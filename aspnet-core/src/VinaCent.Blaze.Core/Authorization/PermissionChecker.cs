using Abp.Authorization;
using VinaCent.Blaze.Authorization.Roles;
using VinaCent.Blaze.Authorization.Users;

namespace VinaCent.Blaze.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

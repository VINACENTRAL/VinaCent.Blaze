using System.Collections.Generic;
using System.Linq;
using VinaCent.Blaze.Roles.Dto;
using VinaCent.Blaze.Users.Dto;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.Users;

public class EditUserModalViewModel
{
    public UserDto User { get; set; }

    public IReadOnlyList<RoleDto> Roles { get; set; }

    public bool UserIsInRole(RoleDto role)
    {
        return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
    }
}

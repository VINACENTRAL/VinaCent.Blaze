using System.Collections.Generic;
using VinaCent.Blaze.Roles.Dto;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.Users;

public class UserListViewModel
{
    public IReadOnlyList<RoleDto> Roles { get; set; }
}

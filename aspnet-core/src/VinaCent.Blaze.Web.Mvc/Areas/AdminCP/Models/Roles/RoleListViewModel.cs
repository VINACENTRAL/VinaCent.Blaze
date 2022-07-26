using System.Collections.Generic;
using VinaCent.Blaze.Roles.Dto;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}

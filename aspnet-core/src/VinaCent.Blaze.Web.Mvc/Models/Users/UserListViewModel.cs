using System.Collections.Generic;
using VinaCent.Blaze.Roles.Dto;

namespace VinaCent.Blaze.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}

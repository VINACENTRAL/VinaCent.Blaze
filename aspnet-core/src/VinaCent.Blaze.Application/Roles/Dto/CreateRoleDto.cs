using System.Collections.Generic;
using Abp.Authorization.Roles;
using VinaCent.Blaze.Authorization.Roles;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Roles.Dto
{
    public class CreateRoleDto
    {
        [AppRequired]
        [AppStringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }
        
        [AppRequired]
        [AppStringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        public string NormalizedName { get; set; }
        
        [AppStringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        public List<string> GrantedPermissions { get; set; }

        public CreateRoleDto()
        {
            GrantedPermissions = new List<string>();
        }
    }
}

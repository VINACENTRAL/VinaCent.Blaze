using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using VinaCent.Blaze.Authorization.Roles;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Roles.Dto
{
    public class RoleEditDto: EntityDto<int>
    {
        [AppRequired]
        [AppStringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }

        [AppRequired]
        [AppStringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [AppStringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        public bool IsStatic { get; set; }
    }
}
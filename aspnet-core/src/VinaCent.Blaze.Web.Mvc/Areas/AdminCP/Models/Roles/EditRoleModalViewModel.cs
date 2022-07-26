using Abp.AutoMapper;
using VinaCent.Blaze.Roles.Dto;
using VinaCent.Blaze.Web.Models.Common;

namespace VinaCent.Blaze.Web.Areas.AdminCP.Models.Roles;

[AutoMapFrom(typeof(GetRoleForEditOutput))]
public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
{
    public bool HasPermission(FlatPermissionDto permission)
    {
        return GrantedPermissionNames.Contains(permission.Name);
    }
}

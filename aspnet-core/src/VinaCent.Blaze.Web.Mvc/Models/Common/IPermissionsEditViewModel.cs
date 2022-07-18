using System.Collections.Generic;
using VinaCent.Blaze.Roles.Dto;

namespace VinaCent.Blaze.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}
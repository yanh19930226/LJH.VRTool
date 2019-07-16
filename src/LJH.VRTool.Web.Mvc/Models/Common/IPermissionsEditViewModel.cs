using System.Collections.Generic;
using LJH.VRTool.Roles.Dto;

namespace LJH.VRTool.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}
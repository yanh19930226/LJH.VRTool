using System.Collections.Generic;
using LJH.VRTool.Roles.Dto;

namespace LJH.VRTool.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleListDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}

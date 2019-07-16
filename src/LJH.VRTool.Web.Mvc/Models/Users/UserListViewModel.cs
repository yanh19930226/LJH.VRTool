using System.Collections.Generic;
using LJH.VRTool.Roles.Dto;
using LJH.VRTool.Users.Dto;

namespace LJH.VRTool.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}

using Abp.Authorization;
using LJH.VRTool.Authorization.Roles;
using LJH.VRTool.Authorization.Users;

namespace LJH.VRTool.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

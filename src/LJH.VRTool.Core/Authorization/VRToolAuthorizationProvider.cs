using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace LJH.VRTool.Authorization
{
    public class VRToolAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {

            #region 基本业务(用户角色权限)
            //用户
            var users = context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            users.CreateChildPermission(PermissionNames.Pages_Users_Search, L("User.Search"));
            users.CreateChildPermission(PermissionNames.Pages_Users_Create, L("User.Create"));
            users.CreateChildPermission(PermissionNames.Pages_Users_Edit, L("User.Edit"));
            users.CreateChildPermission(PermissionNames.Pages_Users_Delete, L("User.Delete"));
            users.CreateChildPermission(PermissionNames.Pages_Users_BatchDelete, L("User.BatchDelete"));
            users.CreateChildPermission(PermissionNames.Pages_Users_IsActive, L("User.IsActive"));

            //角色
            var roles = context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_Search, L("Role.Search"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_Create, L("Role.Create"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_Edit, L("Role.Edit"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_Delete, L("Role.Delete"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_BatchDelete, L("Role.BatchDelete"));

            //租户
            var tenants = context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
           
            context.CreatePermission(PermissionNames.Pages_WebSetting, L("WebSetting"));
            #endregion

            #region 扩展业务
            context.CreatePermission(PermissionNames.Pages_Panoram, L("Panoram"));
            context.CreatePermission(PermissionNames.Pages_Source, L("Source"));
            context.CreatePermission(PermissionNames.Pages_Video, L("Video"));
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, VRToolConsts.LocalizationSourceName);
        }
    }
}

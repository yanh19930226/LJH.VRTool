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
            ///租户管理
            var tenants = context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            ///用户管理
            var users = context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            users.CreateChildPermission(PermissionNames.Pages_Users_SearchAct, L("Users_SearchAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_CreateAct, L("Users_CreateAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_EditAct, L("Users_EditAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_DeleteAct, L("Users_DeleteAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_BatchDeleteAct, L("Users_BatchDeleteAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_IsActiveAct, L("Users_IsActiveAct"));
            ///角色管理
            var roles = context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_SearchAct, L("Roles_SearchAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_CreateAct, L("Roles_CreateAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_EditAct, L("Roles_EditAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_DeleteAct, L("Roles_DeleteAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_BatchDeleteAct, L("Roles_BatchDeleteAct"));
           
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

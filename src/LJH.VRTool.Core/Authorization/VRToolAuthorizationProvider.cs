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
            var basic = context.CreatePermission(PermissionNames.Menus_Basic, L("Menus_Basic"));
            var tenants=basic.CreateChildPermission(PermissionNames.Pages_Tenants, L("Pages_Tenants"), multiTenancySides: MultiTenancySides.Host);
            var users = basic.CreateChildPermission(PermissionNames.Pages_Users, L("Pages_Users"));
            var roles = basic.CreateChildPermission(PermissionNames.Pages_Roles, L("Pages_Roles"));
            var organizations = basic.CreateChildPermission(PermissionNames.Pages_Organizations, L("Pages_Roles"));

            ///租户管理
            //var tenants = context.CreatePermission(PermissionNames.Pages_Tenants, L("Pages_Tenants"), multiTenancySides: MultiTenancySides.Host);
            ///用户管理
            //var users = context.CreatePermission(PermissionNames.Pages_Users, L("Pages_Users"));
            users.CreateChildPermission(PermissionNames.Pages_Users_SearchAct, L("Pages_Users_SearchAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_CreateAct, L("Pages_Users_CreateAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_EditAct, L("Pages_Users_EditAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_DeleteAct, L("Pages_Users_DeleteAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_BatchDeleteAct, L("Pages_Users_BatchDeleteAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_IsActiveAct, L("Pages_Users_IsActiveAct"));
            ///角色管理
            //var roles = context.CreatePermission(PermissionNames.Pages_Roles, L("Pages_Roles"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_SearchAct, L("Pages_Roles_SearchAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_CreateAct, L("Pages_Roles_CreateAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_EditAct, L("Pages_Roles_EditAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_DeleteAct, L("Pages_Roles_DeleteAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_BatchDeleteAct, L("Pages_Roles_BatchDeleteAct"));


            ///组织管理
            //var organizations = context.CreatePermission(PermissionNames.Pages_Organizations, L("Pages_Roles"));
            roles.CreateChildPermission(PermissionNames.Pages_Organizations_SearchAct, L("Pages_Roles_SearchAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Organizations_CreateAct, L("Pages_Roles_CreateAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Organizations_EditAct, L("Pages_Roles_EditAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Organizations_DeleteAct, L("Pages_Roles_DeleteAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Organizations_BatchDeleteAct, L("Pages_Roles_BatchDeleteAct"));

           
            #endregion

            #region 扩展业务
            var logic = context.CreatePermission(PermissionNames.Menus_Logic, L("Menus_Logic"));
            var panoram= logic.CreateChildPermission(PermissionNames.Pages_Panoram, L("Pages_Panoram"));
            var source=logic.CreateChildPermission(PermissionNames.Pages_Source, L("Pages_Source"));
            var video=logic.CreateChildPermission(PermissionNames.Pages_Video, L("Pages_Video"));


            //context.CreatePermission(PermissionNames.Pages_Source, L("Pages_Source"));
            //context.CreatePermission(PermissionNames.Pages_Video, L("Pages_Video"));
            #endregion

            var systemset = context.CreatePermission(PermissionNames.Menus_SystemSet, L("Menus_SystemSet"));
            ///日志管理
            var logs = systemset.CreateChildPermission(PermissionNames.Pages_Logs, L("Pages_Logs"));
            //var logs = context.CreatePermission(PermissionNames.Pages_Logs, L("Pages_Logs"));
            roles.CreateChildPermission(PermissionNames.Pages_Logs_SearchAct, L("Pages_Logs_SearchAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Logs_DetailAct, L("Pages_Logs_DetailAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Logs_DeleteAct, L("Pages_Logs_DeleteAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Logs_BatchDeleteAct, L("Pages_Logs_BatchDeleteAct"));

            ///网站设置
            var websetting= systemset.CreateChildPermission(PermissionNames.Pages_WebSetting, L("WebSetting"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, VRToolConsts.LocalizationSourceName);
        }
    }
}

﻿using Abp.Authorization;
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
            var tenants = context.CreatePermission(PermissionNames.Pages_Tenants, L("Pages_Tenants"), multiTenancySides: MultiTenancySides.Host);
            ///用户管理
            var users = context.CreatePermission(PermissionNames.Pages_Users, L("Pages_Users"));
            users.CreateChildPermission(PermissionNames.Pages_Users_SearchAct, L("Pages_Users_SearchAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_CreateAct, L("Pages_Users_CreateAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_EditAct, L("Pages_Users_EditAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_DeleteAct, L("Pages_Users_DeleteAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_BatchDeleteAct, L("Pages_Users_BatchDeleteAct"));
            users.CreateChildPermission(PermissionNames.Pages_Users_IsActiveAct, L("Pages_Users_IsActiveAct"));
            ///角色管理
            var roles = context.CreatePermission(PermissionNames.Pages_Roles, L("Pages_Roles"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_SearchAct, L("Pages_Roles_SearchAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_CreateAct, L("Pages_Roles_CreateAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_EditAct, L("Pages_Roles_EditAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_DeleteAct, L("Pages_Roles_DeleteAct"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_BatchDeleteAct, L("Pages_Roles_BatchDeleteAct"));
           
            context.CreatePermission(PermissionNames.Pages_WebSetting, L("WebSetting"));
            #endregion

            #region 扩展业务
            context.CreatePermission(PermissionNames.Pages_Panoram, L("Pages_Panoram"));
            context.CreatePermission(PermissionNames.Pages_Source, L("Pages_Source"));
            context.CreatePermission(PermissionNames.Pages_Video, L("Pages_Video"));
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, VRToolConsts.LocalizationSourceName);
        }
    }
}

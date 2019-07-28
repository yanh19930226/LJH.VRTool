namespace LJH.VRTool.Authorization
{
    public static class PermissionNames
    {
        #region 基本业务(用户角色权限)
        /// <summary>
        /// 租户管理
        /// </summary>
        public const string Pages_Tenants = "Pages.Tenants";
        /// <summary>
        /// 用户管理
        /// </summary>
        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_SearchAct = "Pages_Users_SearchAct";
        public const string Pages_Users_CreateAct = "Pages_Users_CreateAct";
        public const string Pages_Users_EditAct = "Pages_Users_EditAct";
        public const string Pages_Users_DeleteAct = "Pages_Users_DeleteAct";
        public const string Pages_Users_BatchDeleteAct = "Pages_Users_BatchDeleteAct";
        public const string Pages_Users_IsActiveAct = "Pages_Users_IsActiveAct";
        /// <summary>
        /// 角色管理
        /// </summary>
        public const string Pages_Roles = "Pages.Roles";
        public const string Pages_Roles_SearchAct = "Pages_Roles_SearchAct";
        public const string Pages_Roles_CreateAct = "Pages_Roles_CreateAct";
        public const string Pages_Roles_EditAct = "Pages_Roles_EditAct";
        public const string Pages_Roles_DeleteAct = "Pages_Roles_DeleteAct";
        public const string Pages_Roles_BatchDeleteAct = "Pages_Roles_BatchDeleteAct";

        public const string Pages_WebSetting = "Pages.WebSetting"; 
        #endregion

        #region 扩展业务
        //全景权限
        public const string Pages_Panoram = "Pages.Panoram";
        //素材权限
        public const string Pages_Source = "Pages.Source";
        //Vr视频权限
        public const string Pages_Video = "Pages.Video"; 
        #endregion
    }
}

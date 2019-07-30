namespace LJH.VRTool.Authorization
{
    public static class PermissionNames
    {
        #region 基本业务(用户角色权限)
        /// <summary>
        /// 租户管理
        /// </summary>
        public const string Pages_Tenants = "Pages_Tenants";
        /// <summary>
        /// 用户管理
        /// </summary>
        public const string Pages_Users = "Pages_Users";
        public const string Pages_Users_SearchAct = "Pages_Users_SearchAct";
        public const string Pages_Users_CreateAct = "Pages_Users_CreateAct";
        public const string Pages_Users_EditAct = "Pages_Users_EditAct";
        public const string Pages_Users_DeleteAct = "Pages_Users_DeleteAct";
        public const string Pages_Users_BatchDeleteAct = "Pages_Users_BatchDeleteAct";
        public const string Pages_Users_IsActiveAct = "Pages_Users_IsActiveAct";
        /// <summary>
        /// 角色管理
        /// </summary>
        public const string Pages_Roles = "Pages_Roles";
        public const string Pages_Roles_SearchAct = "Pages_Roles_SearchAct";
        public const string Pages_Roles_CreateAct = "Pages_Roles_CreateAct";
        public const string Pages_Roles_EditAct = "Pages_Roles_EditAct";
        public const string Pages_Roles_DeleteAct = "Pages_Roles_DeleteAct";
        public const string Pages_Roles_BatchDeleteAct = "Pages_Roles_BatchDeleteAct";
        /// <summary>
        /// 组织管理
        /// </summary>
        public const string Pages_Organizations = "Pages_Organizations";
        public const string Pages_Organizations_SearchAct = "Pages_Organizations_SearchAct";
        public const string Pages_Organizations_CreateAct = "Pages_Organizations_CreateAct";
        public const string Pages_Organizations_EditAct = "Pages_Organizations_EditAct";
        public const string Pages_Organizations_DeleteAct = "Pages_Organizations_DeleteAct";
        public const string Pages_Organizations_BatchDeleteAct = "Pages_Organizations_BatchDeleteAct";

        public const string Pages_WebSetting = "Pages_WebSetting";


        /// <summary>
        /// 日志管理
        /// </summary>
        public const string Pages_Logs = "Pages_Logs";
        public const string Pages_Logs_SearchAct = "Pages_Logs_SearchAct";
        public const string Pages_Logs_DetailAct = "Pages_Logs_DetailAct";
        public const string Pages_Logs_DeleteAct = "Pages_Logs_DeleteAct";
        public const string Pages_Logs_BatchDeleteAct = "Pages_Logs_BatchDeleteAct";

        #endregion

        #region 扩展业务
        //全景权限
        public const string Pages_Panoram = "Pages_Panoram";
        //素材权限
        public const string Pages_Source = "Pages_Source";
        //Vr视频权限
        public const string Pages_Video = "Pages_Video"; 
        #endregion
    }
}

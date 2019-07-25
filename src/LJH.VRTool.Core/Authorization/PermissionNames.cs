namespace LJH.VRTool.Authorization
{
    public static class PermissionNames
    {
        #region 基本业务(用户角色权限)
        public const string Pages_Tenants = "Pages.Tenants";

        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_Search = "Pages.Users_Search";
        public const string Pages_Users_Create = "Pages_Users_Create";
        public const string Pages_Users_Edit = "Pages_Users_Edit";
        public const string Pages_Users_Delete = "Pages_Users_Delete";
        public const string Pages_Users_BatchDelete = "Pages_Users_BatchDelete";
        public const string Pages_Users_IsActive = "Pages_Users_IsActive";

        public const string Pages_Roles = "Pages.Roles";
        public const string Pages_Roles_Search = "Pages.Roles_Search";
        public const string Pages_Roles_Create = "Pages.Roles_Create";
        public const string Pages_Roles_Edit = "Pages.Roles_Edit";
        public const string Pages_Roles_Delete = "Pages.Roles_Delete";
        public const string Pages_Roles_BatchDelete = "Pages.Roles_BatchDelete";


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

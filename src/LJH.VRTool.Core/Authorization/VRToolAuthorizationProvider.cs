using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace LJH.VRTool.Authorization
{
    public class VRToolAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Panoram, L("Panoram"));
            context.CreatePermission(PermissionNames.Pages_Source, L("Source"));
            context.CreatePermission(PermissionNames.Pages_Video, L("Video"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, VRToolConsts.LocalizationSourceName);
        }
    }
}

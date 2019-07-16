using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace LJH.VRTool.Controllers
{
    public abstract class VRToolControllerBase: AbpController
    {
        protected VRToolControllerBase()
        {
            LocalizationSourceName = VRToolConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

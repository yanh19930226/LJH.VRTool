using Abp.AspNetCore.Mvc.ViewComponents;

namespace LJH.VRTool.Web.Views
{
    public abstract class VRToolViewComponent : AbpViewComponent
    {
        protected VRToolViewComponent()
        {
            LocalizationSourceName = VRToolConsts.LocalizationSourceName;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace LJH.VRTool.Web.Views
{
    public abstract class VRToolRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected VRToolRazorPage()
        {
            LocalizationSourceName = VRToolConsts.LocalizationSourceName;
        }
    }
}

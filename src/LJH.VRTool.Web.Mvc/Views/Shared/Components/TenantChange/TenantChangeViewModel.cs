using Abp.AutoMapper;
using LJH.VRTool.Sessions.Dto;

namespace LJH.VRTool.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}

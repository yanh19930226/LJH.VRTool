using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LJH.VRTool.MultiTenancy.Dto;

namespace LJH.VRTool.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}


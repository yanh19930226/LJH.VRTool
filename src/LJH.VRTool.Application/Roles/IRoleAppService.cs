using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LJH.VRTool.Roles.Dto;

namespace LJH.VRTool.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>
    {
        #region MyRegion
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);

        Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input); 
        #endregion

        Task<List<RoleDto>> GetAllListAsync();
        Task<RoleDto> CreateRole(CreateRoleDto input);
        List<RoleDto> GetAllList(string Keyword, DateTime? TimeMin, DateTime? TimeMax);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using LJH.VRTool.Authorization.Roles;
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
        Task<RoleEditDto> GetRoleByAsync(int id);
        Task<Role> GetRoleByIdAsync(int id);
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<Permission> GetAllPermissionsNotMap();
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<GrantedPermission>> GetGrantedPermission(int Id);
    }
}

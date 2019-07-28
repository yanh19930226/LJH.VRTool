using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using LJH.VRTool.Authorization;
using LJH.VRTool.Authorization.Roles;
using LJH.VRTool.Authorization.Users;
using LJH.VRTool.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LJH.VRTool.Roles
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;

        public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        #region MyRegion
        public override async Task<RoleDto> Create(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public async Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
        {
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }

        protected override IQueryable<Role> CreateFilteredQuery(PagedRoleResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.DisplayName.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword));
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedRoleResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        } 
        #endregion

        /// <summary>
        /// 条件搜索
        /// </summary>
        /// <param name="Keyword"></param>
        /// <param name="TimeMin"></param>
        /// <param name="TimeMax"></param>
        /// <returns></returns>
        public List<RoleDto> GetAllList(string Keyword, DateTime? TimeMin, DateTime? TimeMax)
        {
            var users = Repository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(Keyword), q => q.Name.Contains(Keyword) || q.DisplayName.Contains(Keyword))
                .WhereIf(TimeMin.HasValue, q => q.CreationTime >= TimeMin.Value)
                .WhereIf(TimeMax.HasValue, q => q.CreationTime <= TimeMax.Value);
            return new List<RoleDto>(ObjectMapper.Map<List<RoleDto>>(users));
        }
        /// <summary>
        ///异步获取角色
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleDto>> GetAllListAsync()
        {
            var roles = await Repository.GetAllListAsync();
            return new List<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();
            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions)
            ));
        }
        /// <summary>
        /// 获取所有权限(该方法和上面的重复了？)
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Permission> GetAllPermissionsNotMap()
        {
            var per = PermissionManager.GetAllPermissions();
            return per;

        }
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<GrantedPermission>> GetGrantedPermission(int Id)
        {
            var role = await _roleManager.GetRoleByIdAsync(Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role));
            return ObjectMapper.Map<List<GrantedPermission>>(grantedPermissions).ToList();
        }
        /// <summary>
        /// 创建角色并分配权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RoleDto> CreateRole(CreateRoleDto input)
        {
            CheckCreatePermission();
            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
            return MapToEntityDto(role);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async Task<Role> GetRoleByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<RoleEditDto> GetRoleByAsync(int id)
        {
            var role= await Repository.GetAsync(id);
            return ObjectMapper.Map<RoleEditDto>(role);
        }
        public override async Task<RoleDto> Update(RoleDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

    }
}


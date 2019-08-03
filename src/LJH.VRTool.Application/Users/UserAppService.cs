﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using LJH.VRTool.Authorization;
using LJH.VRTool.Authorization.Accounts;
using LJH.VRTool.Authorization.Roles;
using LJH.VRTool.Authorization.Users;
using LJH.VRTool.Roles.Dto;
using LJH.VRTool.Users.Dto;
using LJH.VRTool.Users.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LJH.VRTool.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
        }
        #region MyRegion
        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }
        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }
        #endregion
        
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }
        /// <summary>
        ///异步获取用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDto>> GetAllListAsync()
        {
            var users = await Repository.GetAllListAsync();
            return new List<UserDto>(ObjectMapper.Map<List<UserDto>>(users));
        }
        /// <summary>
        /// 条件搜索
        /// </summary>
        /// <param name="Keyword"></param>
        /// <param name="TimeMin"></param>
        /// <param name="TimeMax"></param>
        /// <returns></returns>
        public List<UserDto> GetAllList(string Keyword,DateTime?TimeMin, DateTime? TimeMax)
        {
            var users = Repository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(Keyword), q => q.Name.Contains(Keyword) || q.UserName.Contains(Keyword) || q.FullName.Contains(Keyword))
                .WhereIf(TimeMin.HasValue, q => q.CreationTime >= TimeMin.Value)
                .WhereIf(TimeMax.HasValue, q => q.CreationTime <= TimeMax.Value);
            return new List<UserDto>(ObjectMapper.Map<List<UserDto>>(users));
        }
        /// <summary>
        /// 根据id 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }
        /// <summary>
        /// 创建用户并分配角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async Task<UserDto> CreateUser(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<UserDto> Update(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }
        /// <summary>
        /// 激活用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         public async Task<long> ChangeActive(long id)
         {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }
            user.IsActive = !user.IsActive;
            return await Repository.InsertOrUpdateAndGetIdAsync(user);
        }
        /// <summary>
        /// 根据组织机构和条件获取数据
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <param name="Keyword"></param>
        /// <param name="TimeMin"></param>
        /// <param name="TimeMax"></param>
        /// <returns></returns>
        public List<UserDto> GetAllListByOrganizationSearch(long? OrganizationId, string Keyword, DateTime? TimeMin, DateTime? TimeMax)
        {
            List<UserDto> list = new List<UserDto>();
            List<UserOrganizationUnit> li = new List<UserOrganizationUnit>();
            if (OrganizationId.HasValue)
            {
                li = _userOrganizationUnitRepository.GetAllList(q => q.OrganizationUnitId == OrganizationId);
            }
            else
            {
                li = _userOrganizationUnitRepository.GetAllList();
            }
            foreach (var item in li)
            {
                var map = ObjectMapper.Map<UserDto>(Repository.Get(item.UserId));
                list.Add(map);
            }
            if (!string.IsNullOrEmpty(Keyword))
            {
                list = list.Where(q => q.Name.Contains(Keyword)).ToList();
            }
            return list;
        }
    }
}


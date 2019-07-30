using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Organizations;
using Abp.Runtime.Session;
using LJH.VRTool.Authorization;
using LJH.VRTool.Organizations.Dto;
using LJH.VRTool.Users.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LJH.VRTool.Organizations
{
    public class OrganizationAppService : AsyncCrudAppService<OrganizationUnit, OrganizationUnitDto, long, PagedUserResultRequestDto>, IOrganizationAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly LogInManager _logInManager;
        private readonly IAbpSession _abpSession;
        //OU仓储，通过此仓储读取OU
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        public OrganizationAppService(IRepository<OrganizationUnit, long> repository, OrganizationUnitManager organizationUnitManager, LogInManager logInManager, IAbpSession abpSession) : base(repository)
        {
            organizationUnitManager = _organizationUnitManager;
            _logInManager = logInManager;
            _abpSession = abpSession;
        }
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        /// <summary>
        /// 获取所有的组织节点
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationUnit>> GetOrganizationList()
        {
            return await Repository.GetAllListAsync();
        }
        /// <summary>
        /// 创建组织机构节点
        /// </summary>
        /// <param name="organizationUnit"></param>
        public OrganizationUnit Create(OrganizationUnitCreateDto organizationUnit)
        {

            
            CheckCreatePermission();
            var org = ObjectMapper.Map<OrganizationUnit>(organizationUnit);
            //org.Code = "0001";
            //org.CreationTime = DateTime.Now;
            return Repository.Insert(org);
             //_organizationUnitManager.CreateAsync(org);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteAsync(EntityDto<long> input)
        {
            CheckDeletePermission();
            await _organizationUnitManager.DeleteAsync(input.Id);
        }

        public async Task Update(OrganizationUnit organizationUnit)
        {
            await _organizationUnitManager.UpdateAsync(organizationUnit);
        }
    }
}

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.ObjectMapping;
using Abp.Organizations;
using Abp.Runtime.Session;
using LJH.VRTool.Authorization;
using LJH.VRTool.Organizations.Dto;
using LJH.VRTool.Roles.Dto;
using LJH.VRTool.Users.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LJH.VRTool.Organizations
{
    public class OrganizationAppService : IOrganizationAppService
    {
        private readonly IRepository<OrganizationUnit, long> _OrganizationUnitRepository;
        private readonly IObjectMapper _objectMapper;
       
        public OrganizationAppService(IRepository<OrganizationUnit, long> OrganizationUnitRepository, IObjectMapper objectMapper)
        {
            _OrganizationUnitRepository = OrganizationUnitRepository;
            _objectMapper = objectMapper;
        }
        public List<OrganizationUnit> GetOrganizationList()
        {
            return  _OrganizationUnitRepository.GetAllList();
        }
        public List<OrganizationUnitDto> GetList()
        {
            return _objectMapper.Map<List<OrganizationUnitDto>>(_OrganizationUnitRepository.GetAllList());
        }
        public long InsertAndGetId(OrganizationUnitCreateDto organizationUnit)
        {
            var org = _objectMapper.Map<OrganizationUnit>(organizationUnit);
            org.Code= OrganizationUnit.CreateCode(2);
            return _OrganizationUnitRepository.InsertAndGetId(org);
        }
        public async Task<OrganizationUnit> UpdateAsync(OrganizationUnitUpdateDto organizationUnit)
        {
            var org=_OrganizationUnitRepository.Get(organizationUnit.Id);
            org = _objectMapper.Map<OrganizationUnit>(organizationUnit);
            return await _OrganizationUnitRepository.InsertOrUpdateAsync(org);
        }
        public async Task DeleteAsync(long Id)
        {
            await _OrganizationUnitRepository.DeleteAsync(Id);
        }
    }
}

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Organizations;
using LJH.VRTool.Organizations.Dto;
using LJH.VRTool.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LJH.VRTool.Organizations
{
    public class OrganizationAppService : AsyncCrudAppService<OrganizationUnit, OrganizationUnitDto, long, PagedUserResultRequestDto>, IOrganizationAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        //OU仓储，通过此仓储读取OU
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        public OrganizationAppService(IRepository<OrganizationUnit, long> repository, OrganizationUnitManager organizationUnitManager) : base(repository)
        {
            organizationUnitManager = _organizationUnitManager;
        }
        public void Test()
        {
            
        }
    }
}

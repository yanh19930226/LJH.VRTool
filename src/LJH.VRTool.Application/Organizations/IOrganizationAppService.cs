using Abp.Application.Services;
using Abp.Organizations;
using LJH.VRTool.Organizations.Dto;
using LJH.VRTool.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LJH.VRTool.Organizations
{
    public interface IOrganizationAppService : IApplicationService
    {
        List<OrganizationUnit> GetOrganizationList();

        List<OrganizationUnitDto> GetList();
        long InsertAndGetId(OrganizationUnitCreateDto organizationUnit);
        Task<OrganizationUnit> UpdateAsync(OrganizationUnitUpdateDto organizationUnit);
        Task DeleteAsync(long Id);
    }
}

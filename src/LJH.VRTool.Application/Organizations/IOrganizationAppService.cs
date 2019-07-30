using Abp.Organizations;
using LJH.VRTool.Organizations.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LJH.VRTool.Organizations
{
    public interface IOrganizationAppService
    {
        Task<List<OrganizationUnit>> GetOrganizationList();
        //Task<OrganizationUnit> CreateAsync(OrganizationUnitCreateDto organizationUnit);
        OrganizationUnit Create(OrganizationUnitCreateDto organizationUnit);
    }
}

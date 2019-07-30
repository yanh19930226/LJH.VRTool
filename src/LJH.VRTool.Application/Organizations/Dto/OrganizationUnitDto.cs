using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace LJH.VRTool.Organizations.Dto
{
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : EntityDto<long>
    {
    }
}

using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LJH.VRTool.Organizations.Dto
{
    [AutoMapTo(typeof(OrganizationUnit))]
    public class OrganizationUnitUpdateDto :EntityDto<long>
    {
        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; }
    }
}

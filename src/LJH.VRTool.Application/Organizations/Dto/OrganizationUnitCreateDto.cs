using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LJH.VRTool.Organizations.Dto
{
    [AutoMapTo(typeof(OrganizationUnit))]
    public class OrganizationUnitCreateDto
    {
        public long? ParentId { get; set; }
        public int? TenantId { get; set; }
        [Required]
        [StringLength(128)]
        public  string DisplayName { get; set; }
    }
}

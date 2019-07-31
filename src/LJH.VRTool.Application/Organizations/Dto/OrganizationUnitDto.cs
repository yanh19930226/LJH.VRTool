using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LJH.VRTool.Organizations.Dto
{
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : EntityDto<long>
    {
        public  string Code { get; set; }
        public  long? ParentId { get; set; }
        
        public  int? TenantId { get; set; }
        //[JsonIgnore]
        public List<OrganizationUnitDto> Children { get; set; }
        [Required]
        [StringLength(128)]
        public  string DisplayName { get; set; }
    }
}

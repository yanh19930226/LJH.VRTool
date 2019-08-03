using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LJH.VRTool.Users.Dto
{
    public class OrganizationUserDto : EntityDto<long>
    {
        public string Name { get; set; }

        public DateTime CreationTime { get; set; }
    }
}

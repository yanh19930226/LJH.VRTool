using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LJH.VRTool.Entities.UserStudent
{
    public class UserStudent : Entity, IHasCreationTime
    {
        public DateTime CreationTime { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

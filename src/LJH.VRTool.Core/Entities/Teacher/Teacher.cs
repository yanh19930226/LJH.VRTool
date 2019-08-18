using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LJH.VRTool.Entities.Teacher
{
    public class Teacher : Entity, IHasCreationTime
    {
        [Required]
        public int Age { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime CreationTime { get; set; }
        public virtual ICollection<LJH.VRTool.Entities.StudentTeacher.StudentTeacher> StudentTeachers { get; set; }
    }
}

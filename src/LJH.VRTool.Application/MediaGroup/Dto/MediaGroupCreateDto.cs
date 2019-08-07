using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LJH.VRTool.MediaGroup.Dto
{
    public class MediaGroupCreateDto
    {
        public int? MediaType { get; set; }
        [Required]
        [StringLength(20)]
        public string Title { get; set; }
    }
}

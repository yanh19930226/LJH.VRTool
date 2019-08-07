using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LJH.VRTool.MediaGroup.Dto
{
    public class MediaGroupUpdateDto : EntityDto<long>
    {
        [Required]
        [StringLength(20)]
        public string Title { get; set; }
    }
}

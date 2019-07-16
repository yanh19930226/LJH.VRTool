using Abp.Application.Services.Dto;

namespace LJH.VRTool.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}


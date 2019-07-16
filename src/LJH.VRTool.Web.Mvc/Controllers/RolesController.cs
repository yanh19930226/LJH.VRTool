using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Roles;
using LJH.VRTool.Roles.Dto;
using LJH.VRTool.Web.Models.Roles;
using AspNetCorePage;
using System.Linq;

namespace LJH.VRTool.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Roles)]
    public class RolesController : VRToolControllerBase
    {
        private readonly IRoleAppService _roleAppService;

        public RolesController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }
        public async Task<ActionResult> Index(int pageIndex = 1)
        {
            int pageSize = 1;
            var roles = (await _roleAppService.GetAllListAsync());
            PagedList<RoleDto> model = roles.OrderBy(a => a.Id).ToPagedList(pageIndex, pageSize);
            return View(model);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(CreateRoleDto model)
        {
            var user = (await _roleAppService.CreateRole(model));
            return Json(new { status = "ok" });
        }


        //public async Task<IActionResult> Index()
        //{
        //    var roles = (await _roleAppService.GetRolesAsync(new GetRolesInput())).Items;
        //    var permissions = (await _roleAppService.GetAllPermissions()).Items;
        //    var model = new RoleListViewModel
        //    {
        //        Roles = roles,
        //        Permissions = permissions
        //    };

        //    return View(model);
        //}

        //public async Task<ActionResult> EditRoleModal(int roleId)
        //{
        //    var output = await _roleAppService.GetRoleForEdit(new EntityDto(roleId));
        //    var model = new EditRoleModalViewModel(output);

        //    return View("_EditRoleModal", model);
        //}
    }
}

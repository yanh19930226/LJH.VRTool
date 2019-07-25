using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Roles;
using LJH.VRTool.Roles.Dto;
using LJH.VRTool.Web.Models.Roles;
using System.Linq;
using Webdiyer.AspNetCore;

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
        public ActionResult Index(RoleSearch search,int pageIndex )
        {
            var roles = _roleAppService.GetAllList(search.KeyWord, search.TimeMin, search.TimeMax);
            PagedList<RoleDto> model = roles.OrderBy(a => a.Id).ToPagedList(pageIndex, search.pageSize);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("List", model);
            }
            ViewBag.Title = "½ÇÉ«¹ÜÀí";
            return View(model);
        }
        public async Task<ActionResult> Add()
        {
            var pers = (await _roleAppService.GetAllPermissions()).Items;
            return View(pers);
        }
        [HttpPost]
        public async Task<ActionResult> Add(CreateRoleDto model)
        {
            model.DisplayName = model.Name;
            var role = (await _roleAppService.CreateRole(model));
            return Json(new { status = "ok" });
        }
        public async Task<ActionResult> Edit(int roleId)
        {
            var output = await _roleAppService.GetRoleForEdit(new EntityDto(roleId));
            var model = new EditRoleModalViewModel(output);

            return View("_EditRoleModal", model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(CreateRoleDto model)
        {
            var user = (await _roleAppService.CreateRole(model));
            return Json(new { status = "ok" });
        }
        [HttpPost]
        public async Task<ActionResult> Delete(EntityDto<int> input)
        {
            await _roleAppService.Delete(input);
            return Json(new { status = "ok" });
        }
        [HttpPost]
        public async Task<ActionResult> BatchDelete(int[] selectedIds)
        {
            foreach (int id in selectedIds)
            {
                await _roleAppService.Delete(new EntityDto<int>() { Id = id });
            }
            return Json(new { status = "ok" });
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Users;
using LJH.VRTool.Web.Models.Users;
using LJH.VRTool.Users.Dto;
using Microsoft.AspNetCore.Hosting;
using AspNetCorePage;
using LJH.VRTool.Web.Models.Test;
using System.IO;
using System.Linq;
using LJH.VRTool.Roles;
using Abp.AspNetCore.Mvc.Controllers;
using SearchOption = LJH.VRTool.Web.Models.Users.SearchOption;

namespace LJH.VRTool.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : VRToolControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        public ActionResult Index(int pageIndx,SearchOption search)
        {
            var users =_userAppService.GetAllList(search.KeyWord, search.TimeMin, search.TimeMax);
            PagedList <UserDto> model = users.OrderBy(a => a.CreationTime).ToPagedList(search.pageIndex, search.pageSize);
            return View(model);
        }
        public async Task<ActionResult> Add()
        {
            var roles = (await _userAppService.GetRoles()).Items;
            return View(roles);
        }
        [HttpPost]
        public async Task<ActionResult> Add(CreateUserDto model)
        {
            var user = (await _userAppService.CreateUser(model));
            return Json(new { status = "ok" });
        }
        [HttpPost]
        public async Task<ActionResult> Edit(long userId)
        {
            var user = await _userAppService.Get(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles
            };
            return View("_EditUserModal", model);
        }
        public async Task<ActionResult> Edit()
        {
            var roles = (await _userAppService.GetRoles()).Items;
            return View(roles);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(EntityDto<long> input)
        {
            await _userAppService.Delete(input);
            return Json(new { status = "ok" });
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="selectedIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> BatchDelete(long[] selectedIds)
        {
            foreach (long id in selectedIds)
            {
                await _userAppService.Delete(new EntityDto<long>() { Id = id });
            }
            return Json(new { status = "ok" });
        }
    }
}

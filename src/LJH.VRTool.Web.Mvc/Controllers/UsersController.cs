using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Users;
using LJH.VRTool.Web.Models.Users;
using LJH.VRTool.Users.Dto;
using System.Linq;
using Webdiyer.AspNetCore;
using LJH.VRTool.HttpService;

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
        public ActionResult Index(UserSearch search, int pageIndex)
        {
            var users = _userAppService.GetAllList(search.KeyWord, search.TimeMin, search.TimeMax);
            PagedList<UserDto> model = users.OrderBy(a => a.CreationTime).ToPagedList(pageIndex, search.pageSize);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("List", model);
            }
            ViewBag.Title = "用户管理";
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
        [HttpPost]
        public async Task<ActionResult> BatchDelete(long[] selectedIds)
        {
            foreach (long id in selectedIds)
            {
                await _userAppService.Delete(new EntityDto<long>() { Id = id });
            }
            return Json(new { status = "ok" });
        }
        [HttpPost]
        public ActionResult Test()
        {
            var a=HttpHelper.HttpGet("https://api.douban.com/v2/movie/new_movies?apikey=0b2bdeda43b5688921839c8ecb20399b", null);
            return null;
        }
        public ActionResult TestPost()
        {

            var a = HttpHelper.HttpPostAsync("https://api.douban.com/v2/movie/new_movies?apikey=0b2bdeda43b5688921839c8ecb20399b", null);
            return null;
        }
    }
}

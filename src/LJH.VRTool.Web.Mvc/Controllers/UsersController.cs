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
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Abp.Authorization;

namespace LJH.VRTool.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : VRToolControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UsersController(IUserAppService userAppService, IHostingEnvironment hostingEnvironment)
        {
            _userAppService = userAppService;
            _hostingEnvironment = hostingEnvironment;
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
        [AbpMvcAuthorize(PermissionNames.Pages_Users_Create)]
        public async Task<ActionResult> Add()
        {
            var roles = (await _userAppService.GetRoles()).Items;
            return View(roles);
        }
        [AbpMvcAuthorize(PermissionNames.Pages_Users_Create)]
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
        public async Task<ActionResult> IsActive(long Id)
        {
            var user= await _userAppService.ChangeActive(Id);
            return Json(new { status = "ok" });
        }
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult ChangePwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Test()
        {
            var API_KEY = "2AakWsz5sF1Mj9gKhWqqjLMG";
            var SECRET_KEY = "mTqOIslrK5nIFmcsvPPtXnRgyYY0yFGI";
            var client = new Baidu.Aip.Speech.Tts(API_KEY, SECRET_KEY);
            var option = new Dictionary<string, object>()
            {
                {"spd", 5}, // 语速
                {"vol", 7}, // 音量
                {"per", 4}  // 发音人，4：情感度丫丫童声
            };
            var result = client.Synthesis("众里寻他千百度", option);

            if (result.Success)  // 或 result.Success
            {
                System.IO.File.WriteAllBytes(_hostingEnvironment.WebRootPath+"tt.mp3", result.Data);
            }
            return null;
        }
        public ActionResult TestPost()
        {
            return null;
        }
    }
}

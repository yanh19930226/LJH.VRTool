using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Users;
using LJH.VRTool.Users.Dto;
using Microsoft.AspNetCore.Mvc;
using Webdiyer.AspNetCore;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_WebSetting)]
    public class WebSettingController : VRToolControllerBase
    {
        private readonly IUserAppService _userAppService;

        public WebSettingController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<ActionResult> Index(int pageIndex = 1)
        {
            //int pageSize = 1;
            //var users = (await _userAppService.GetAllListAsync());
            //PagedList<Users.Dto.UserDto> model = users.OrderBy(a => a.CreationTime).ToPagedList(pageIndex, pageSize);
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(CreateUserDto model)
        {
            var user = (await _userAppService.CreateUser(model));
            return Json(new { status = "ok" });
        }
    }
}
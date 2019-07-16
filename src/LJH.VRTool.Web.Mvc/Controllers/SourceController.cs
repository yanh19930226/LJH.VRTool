using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using AspNetCorePage;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Users;
using LJH.VRTool.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Source)]
    public class SourceController : VRToolControllerBase
    {
        private readonly IUserAppService _userAppService;

        public SourceController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        public async Task<ActionResult> Index(int pageIndex = 1)
        {
            int pageSize = 1;
            var users = (await _userAppService.GetAllListAsync());
            PagedList<UserDto> model = users.OrderBy(a => a.CreationTime).ToPagedList(pageIndex, pageSize);
            return View(model);
        }
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete()
        {
            return Json(null);
        }
        public ActionResult Conver()
        {
            return View();
        }
        private void BaiduConver()
        {
            
        }
    }
}
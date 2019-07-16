using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using AspNetCorePage;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Users.Dto;
using LJH.VRTool.Web.Models.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Panoram)]
    public class PanoramController : VRToolControllerBase
    {

        private readonly Users.IUserAppService _userAppService;

        public PanoramController(Users.IUserAppService userAppService)
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
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
    }
}
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
        [HttpPost]
        public async Task<ActionResult> Add(CreateUserDto model)
        {
            var user= (await _userAppService.CreateUser(model));
            return Json(new { status = "ok" });
        }
        //public async Task<ActionResult> EditUserModal(long userId)
        //{
        //    var user = await _userAppService.Get(new EntityDto<long>(userId));
        //    var roles = (await _userAppService.GetRoles()).Items;
        //    var model = new EditUserModalViewModel
        //    {
        //        User = user,
        //        Roles = roles
        //    };
        //    return View("_EditUserModal", model);
        //}
    }
}

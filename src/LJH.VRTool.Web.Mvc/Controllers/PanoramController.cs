using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Users.Dto;
using Microsoft.AspNetCore.Mvc;
using Webdiyer.AspNetCore;

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
        /// <summary>
        /// 基本设置
        /// </summary>
        /// <returns></returns>
        public ActionResult openMoreSetting()
        {
            return View();
        }
        public ActionResult Basic()
        {
            return PartialView("Basic");
        }
        /// <summary>
        /// 背景音乐
        /// </summary>
        /// <returns></returns>
        public ActionResult openMusicSetting()
        {
            return View();
        }
        /// <summary>
        /// 全景素材
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomMedia(string type)
        {
            if (type=="images_select")
            {
                return View("Images");
            }
            else if(type == "audio_select")
            {
                return View("Audio");
            }
            else if (type == "video_select")
            {
                return View("Video");
            }
            else if (type == "ebook_select")
            {
                return View("Ebook");
            }
            else
            {
                return View("");
            }
        }
        /// <summary>
        /// 全景特效
        /// </summary>
        /// <returns></returns>
        public ActionResult Effectseting()
        {
            return View();
        }
        /// <summary>
        /// 遮罩
        /// </summary>
        /// <returns></returns>
        public ActionResult Zhezhao()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Controllers;

namespace LJH.VRTool.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : VRToolControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}

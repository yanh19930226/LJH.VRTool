using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Controllers;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : VRToolControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}

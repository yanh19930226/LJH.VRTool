using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LJH.VRTool.Web.Mvc.Areas.Web.Controllers
{
    public class HomeController : Controller
    {
        [Area("Web")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
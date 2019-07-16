using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LJH.VRTool.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    public class GroupController : VRToolControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(null);
        }
    }
}
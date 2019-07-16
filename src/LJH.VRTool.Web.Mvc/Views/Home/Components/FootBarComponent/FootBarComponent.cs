using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Views.Home.Components.FootBarComponent
{
    public class FootBarComponent : VRToolViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

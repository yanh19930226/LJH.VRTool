using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Views.Home.Components.TopBarComponent
{
    public class TopBarComponent : VRToolViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

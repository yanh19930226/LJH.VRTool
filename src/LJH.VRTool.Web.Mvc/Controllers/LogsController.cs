﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    public class LogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
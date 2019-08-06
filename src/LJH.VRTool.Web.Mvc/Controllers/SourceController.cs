using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Users;
using LJH.VRTool.Users.Dto;
using LJH.VRTool.Web.Models.Source;
using Microsoft.AspNetCore.Mvc;
using Webdiyer.AspNetCore;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Source)]
    public class SourceController : VRToolControllerBase
    {
        private readonly IUserAppService _userAppService;

        public SourceController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        public ActionResult Index(SourceSearch search)
        {
            if (search.MediaType==null)
            {
                search.MediaType = 1;
            }
            SourceViewModel vmodel = new SourceViewModel();
            if (search.MediaType == 1)
            {
                List<TestPanoram> plist = new List<TestPanoram>()
                {
                    new TestPanoram{Id=1,Title="全景图1",Url="",CreateTime="2019-08-06" },
                    new TestPanoram{Id=2,Title="全景图2",Url="",CreateTime="2019-08-06" },
                    new TestPanoram{Id=3,Title="全景图3",Url="",CreateTime="2019-08-06" },
                    new TestPanoram{Id=4,Title="全景图4",Url="",CreateTime="2019-08-06" },
                    new TestPanoram{Id=5,Title="全景图5",Url="",CreateTime="2019-08-06" },
                };
                var slist=ObjectMapper.Map<List<Source>>(plist);
                PagedList<Source> model = slist.OrderBy(q => q.Id).ToPagedList(search.pageIndex,search.pageSize);
                vmodel.MediaType = (int)search.MediaType;
                vmodel.sourcelist = model;
            }
            else
            {
                List<TestAudio> alist = new List<TestAudio>()
                {
                    new TestAudio{Id=1,Title="音频1",Url="",CreateTime="2019-08-06" },
                    new TestAudio{Id=2,Title="音频2",Url="",CreateTime="2019-08-06" },
                    new TestAudio{Id=3,Title="音频3",Url="",CreateTime="2019-08-06" },
                    new TestAudio{Id=4,Title="音频4",Url="",CreateTime="2019-08-06" },
                    new TestAudio{Id=5,Title="音频5",Url="",CreateTime="2019-08-06" },
                };
                var slist = ObjectMapper.Map<List<Source>>(alist);
                PagedList<Source> model = slist.OrderBy(q => q.Id).ToPagedList(search.pageIndex, search.pageSize);
                vmodel.MediaType = (int)search.MediaType;
                vmodel.sourcelist = model;
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (search.MediaType == 1)
                {
                    return PartialView("PanoramList", vmodel);
                }
                else
                {
                    return PartialView("AudioList", vmodel);
                }
            }
            return View(vmodel);
        }
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete()
        {
            return Json(null);
        }
        public ActionResult Conver()
        {
            return View();
        }
        private void BaiduConver()
        {

        }
    }
    /// <summary>
    /// 全景图
    /// </summary>
    public class TestPanoram
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string CreateTime { get; set; }
    }
    /// <summary>
    ///音频
    /// </summary>
    public class TestAudio
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string CreateTime { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using LJH.VRTool.Authorization;
using LJH.VRTool.Controllers;
using LJH.VRTool.Users;
using LJH.VRTool.Users.Dto;
using LJH.VRTool.Web.Models.Common.Tree;
using LJH.VRTool.Web.Models.Source;
using Microsoft.AspNetCore.Mvc;
using Webdiyer.AspNetCore;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Source)]
    public class SourceController : VRToolControllerBase
    {
        private readonly IUserAppService _userAppService;
        #region 初始化数据
        /// <summary>
        /// 全景图表
        /// </summary>
        public class TestPanoram
        {
            public int Id { get; set; }
            public int MediaGroupId { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public string CreateTime { get; set; }
        }
        /// <summary>
        ///图片表
        /// </summary>
        public class TestPicture
        {
            public int Id { get; set; }
            public int MediaGroupId { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public string CreateTime { get; set; }
        }
        /// <summary>
        ///音频表
        /// </summary>
        public class TestAudio
        {
            public int Id { get; set; }
            public int MediaGroupId { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public string CreateTime { get; set; }
        }
        /// <summary>
        ///视频表
        /// </summary>
        public class TestVideo
        {
            public int Id { get; set; }
            public int MediaGroupId { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public string CreateTime { get; set; }
        }
        //全景图初始化数据
        List<TestPanoram> plist = new List<TestPanoram>()
                {
                    new TestPanoram{Id=1,Title="全景图1",Url="",CreateTime="2019-08-06",MediaGroupId=1},
                    new TestPanoram{Id=2,Title="全景图2",Url="",CreateTime="2019-08-06",MediaGroupId=1},
                    new TestPanoram{Id=3,Title="全景图3",Url="",CreateTime="2019-08-06",MediaGroupId=1},
                    new TestPanoram{Id=4,Title="全景图4",Url="",CreateTime="2019-08-06",MediaGroupId=1},
                    new TestPanoram{Id=5,Title="全景图5",Url="",CreateTime="2019-08-06",MediaGroupId=1},
                    new TestPanoram{Id=6,Title="全景图6",Url="",CreateTime="2019-08-06",MediaGroupId=2},
                    new TestPanoram{Id=7,Title="全景图7",Url="",CreateTime="2019-08-06",MediaGroupId=2},
                    new TestPanoram{Id=8,Title="全景图8",Url="",CreateTime="2019-08-06",MediaGroupId=2 },
                    new TestPanoram{Id=9,Title="全景图9",Url="",CreateTime="2019-08-06",MediaGroupId=2 },
                    new TestPanoram{Id=10,Title="全景图10",Url="",CreateTime="2019-08-06",MediaGroupId=4 },
                    new TestPanoram{Id=11,Title="全景图11",Url="",CreateTime="2019-08-06",MediaGroupId=4 },
                    new TestPanoram{Id=12,Title="全景图12",Url="",CreateTime="2019-08-06",MediaGroupId=4 },
                    new TestPanoram{Id=13,Title="全景图13",Url="",CreateTime="2019-08-06",MediaGroupId=4 },
                    new TestPanoram{Id=14,Title="全景图14",Url="",CreateTime="2019-08-06",MediaGroupId=4 },
                    new TestPanoram{Id=15,Title="全景图15",Url="",CreateTime="2019-08-06",MediaGroupId=4 },
                };
        //图片初始化数据
        List<TestPicture> piclist = new List<TestPicture>()
        {
            new TestPicture{Id=1,Title="图片1",Url="",CreateTime="2019-08-06",MediaGroupId=5 },
            new TestPicture{Id=2,Title="图片2",Url="",CreateTime="2019-08-06",MediaGroupId=5 },
            new TestPicture{Id=3,Title="图片3",Url="",CreateTime="2019-08-06",MediaGroupId=5 },
            new TestPicture{Id=4,Title="图片4",Url="",CreateTime="2019-08-06",MediaGroupId=5 },
            new TestPicture{Id=5,Title="图片5",Url="",CreateTime="2019-08-06",MediaGroupId=5 },
        };
        //音频初始化数据
        List<TestAudio> alist = new List<TestAudio>()
        {
            new TestAudio{Id=1,Title="音频1",Url="",CreateTime="2019-08-06",MediaGroupId=9},
            new TestAudio{Id=2,Title="音频2",Url="",CreateTime="2019-08-06" ,MediaGroupId=9},
            new TestAudio{Id=3,Title="音频3",Url="",CreateTime="2019-08-06",MediaGroupId=9 },
            new TestAudio{Id=4,Title="音频4",Url="",CreateTime="2019-08-06",MediaGroupId=9 },
            new TestAudio{Id=5,Title="音频5",Url="",CreateTime="2019-08-06" ,MediaGroupId=9},
            new TestAudio{Id=6,Title="音频6",Url="",CreateTime="2019-08-06",MediaGroupId=9},
            new TestAudio{Id=7,Title="音频7",Url="",CreateTime="2019-08-06" ,MediaGroupId=9},
            new TestAudio{Id=8,Title="音频8",Url="",CreateTime="2019-08-06",MediaGroupId=9 },
            new TestAudio{Id=9,Title="音频9",Url="",CreateTime="2019-08-06",MediaGroupId=9 },
            new TestAudio{Id=10,Title="音频10",Url="",CreateTime="2019-08-06" ,MediaGroupId=9},
            new TestAudio{Id=11,Title="音频11",Url="",CreateTime="2019-08-06",MediaGroupId=9},
            new TestAudio{Id=12,Title="音频12",Url="",CreateTime="2019-08-06" ,MediaGroupId=9},
            new TestAudio{Id=13,Title="音频13",Url="",CreateTime="2019-08-06",MediaGroupId=9 },
            new TestAudio{Id=14,Title="音频14",Url="",CreateTime="2019-08-06",MediaGroupId=9 },
            new TestAudio{Id=15,Title="音频15",Url="",CreateTime="2019-08-06" ,MediaGroupId=9},
        };
        //视频初始化数据
        List<TestVideo> vlist = new List<TestVideo>()
        {
            new TestVideo{Id=1,Title="视频1",Url="",CreateTime="2019-08-06" ,MediaGroupId=15},
            new TestVideo{Id=2,Title="视频2",Url="",CreateTime="2019-08-06",MediaGroupId=15 },
            new TestVideo{Id=3,Title="视频3",Url="",CreateTime="2019-08-06",MediaGroupId=15 },
            new TestVideo{Id=4,Title="视频4",Url="",CreateTime="2019-08-06" ,MediaGroupId=15},
            new TestVideo{Id=5,Title="视频5",Url="",CreateTime="2019-08-06",MediaGroupId=15 },
        }; 
        #endregion
        public SourceController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        public ActionResult Index(SourceSearch search)
        {
            //真实数据MediaGroupId为空，通过底层Whereif方法进行过滤
            if (search.MediaType == null)
            {
                search.MediaType = 1;
            }
            SourceViewModel vmodel = new SourceViewModel();
            if (search.MediaType == 1)
            {
                var slist = ObjectMapper.Map<List<Source>>(plist.Where(q=>q.MediaGroupId==search.MediaGroupId));
                PagedList<Source> model = slist.OrderBy(q => q.Id).ToPagedList(search.pageIndex, search.pageSize);
                vmodel.MediaType = (int)search.MediaType;
                vmodel.sourcelist = model;
            }
            else if (search.MediaType == 2)
            {
                var slist = ObjectMapper.Map<List<Source>>(piclist.Where(q => q.MediaGroupId == search.MediaGroupId));
                PagedList<Source> model = slist.OrderBy(q => q.Id).ToPagedList(search.pageIndex, search.pageSize);
                vmodel.MediaType = (int)search.MediaType;
                vmodel.sourcelist = model;
            }
            else if (search.MediaType == 3)
            {
                var slist = ObjectMapper.Map<List<Source>>(alist.Where(q => q.MediaGroupId == search.MediaGroupId));
                PagedList<Source> model = slist.OrderBy(q => q.Id).ToPagedList(search.pageIndex, search.pageSize);
                vmodel.MediaType = (int)search.MediaType;
                vmodel.sourcelist = model;
            }
            else
            {
                var slist = ObjectMapper.Map<List<Source>>(vlist.Where(q => q.MediaGroupId == search.MediaGroupId));
                PagedList<Source> model = slist.OrderBy(q => q.Id).ToPagedList(search.pageIndex, search.pageSize);
                vmodel.MediaType = (int)search.MediaType;
                vmodel.sourcelist = model;
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                switch (search.MediaType)
                {
                    case 1:
                        return PartialView("PanoramList", vmodel.sourcelist);
                    case 2:
                        return PartialView("PictureList", vmodel.sourcelist);
                    case 3:
                        return PartialView("AudioList", vmodel.sourcelist);
                    case 4:
                        return PartialView("VideoList", vmodel.sourcelist);
                    default:
                        break;
                }
            }
            return View(vmodel);
        }
        [HttpPost]
        public ActionResult Add()
        {
            return Json(null);
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
    
}
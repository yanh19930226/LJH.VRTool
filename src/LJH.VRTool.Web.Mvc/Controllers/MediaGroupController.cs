using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LJH.VRTool.Controllers;
using LJH.VRTool.MediaGroup.Dto;
using LJH.VRTool.Web.Models.Common.Tree;
using Microsoft.AspNetCore.Mvc;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    public class MediaGroupController : VRToolControllerBase
    {

        #region 媒体文件分组表
        /// <summary>
        /// 媒体文件分组表
        /// </summary>
        public class MediaGroup
        {
            public int Id { get; set; }
            public int MediaType { get; set; }
            public string Title { get; set; }
            public string CreateTime { get; set; }
        }
        //分组初始化数据
        List<MediaGroup> grouplist = new List<MediaGroup>()
        {
            //全景分组
            new MediaGroup{Id=1,MediaType=1,Title="全景分组1",CreateTime="2019-08-06"},
            new MediaGroup{Id=2,MediaType=1,Title="全景分组2",CreateTime="2019-08-06"},
            new MediaGroup{Id=3,MediaType=1,Title="全景分组3",CreateTime="2019-08-06"},
            new MediaGroup{Id=4,MediaType=1,Title="全景分组4",CreateTime="2019-08-06"},
            //图片分组
            new MediaGroup{Id=5,MediaType=2,Title="图片分组1",CreateTime="2019-08-06"},
            new MediaGroup{Id=6,MediaType=2,Title="图片分组2",CreateTime="2019-08-06"},
            new MediaGroup{Id=7,MediaType=2,Title="图片分组3",CreateTime="2019-08-06"},
            new MediaGroup{Id=8,MediaType=2,Title="图片分组4",CreateTime="2019-08-06"},
            //音频分组
            new MediaGroup{Id=9,MediaType=3,Title="音频分组1",CreateTime="2019-08-06"},
            new MediaGroup{Id=10,MediaType=3,Title="音频分组2",CreateTime="2019-08-06"},
            new MediaGroup{Id=11,MediaType=3,Title="音频分组3",CreateTime="2019-08-06"},
            new MediaGroup{Id=12,MediaType=3,Title="音频分组4",CreateTime="2019-08-06"},
            //视频分组
            new MediaGroup{Id=13,MediaType=4,Title="视频分组1",CreateTime="2019-08-06"},
            new MediaGroup{Id=14,MediaType=4,Title="视频分组2",CreateTime="2019-08-06"},
            new MediaGroup{Id=15,MediaType=4,Title="视频分组3",CreateTime="2019-08-06"},
            new MediaGroup{Id=16,MediaType=4,Title="视频分组4",CreateTime="2019-08-06"},
        };
        /// <summary>
        /// 根据媒体类型返回媒体分组
        /// </summary>
        /// <param name="MediaType"></param>
        /// <returns></returns> 
	#endregion
        [HttpPost]
        public ActionResult Index(int MediaType)
        {
            var reslist = grouplist.Where(q => q.MediaType == MediaType);
            //将分组数据转为ztree无上下级结构数据
            List<ZTreeItem> treelist = new List<ZTreeItem>();
            foreach (var item in reslist)
            {
                ZTreeItem tree = new ZTreeItem();
                tree.Id = item.Id;
                tree.pId = 0;
                tree.Name = item.Title;
                treelist.Add(tree);
            }
            return Json(new { status = "ok", zNodes = treelist });
        }
        [HttpPost]
        public ActionResult Add(MediaGroupCreateDto create)
        {
            Random rand = new Random();
            var newgroupId = rand.Next(100);
            return Json(new { status = "ok", id = newgroupId });
        }
        [HttpPost]
        public ActionResult Edit(MediaGroupUpdateDto edit)
        {
            return Json(new { status = "ok" });
        }
        [HttpPost]
        public ActionResult Delete(long Id)
        {
            return Json(new { status = "ok"});
        }
    }
}
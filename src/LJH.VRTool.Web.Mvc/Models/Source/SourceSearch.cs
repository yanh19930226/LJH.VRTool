using LJH.VRTool.Web.Models.Common.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Models.Source
{
    public class SourceSearch: Search
    {
        /// <summary>
        /// 多媒体类型(全景图片、图片、音频、视频)
        /// </summary>
        public int? MediaType { get; set; }
        /// <summary>
        /// 所属分组
        /// </summary>
        public int? MediaGroupId { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
    }
}

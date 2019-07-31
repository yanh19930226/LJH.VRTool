using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Models.Common.Tree
{
    public class TreeItem
    {
        /// <summary>
        /// 主键
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        public bool Checked{get;set;}
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool Spread { get; set; }
        /// <summary>
        /// 节点链接
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 子节点 
        /// </summary>
        public List<TreeItem> Children { get; set; }

    }

    public class EleTreeItem
    {
        /// <summary>
        /// 主键
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Label { get; set; }

        public bool Checked { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 子节点 
        /// </summary>
        public List<EleTreeItem> Children { get; set; }
    }
    public class DTreeItem
    {
        /// <summary>
        /// 主键
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        public int? CheckArr { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 子节点 
        /// </summary>
        public List<DTreeItem> Children { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public string IconClass { get; set; }
    }
}

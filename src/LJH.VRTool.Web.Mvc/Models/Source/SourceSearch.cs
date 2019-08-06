using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Models.Source
{
    public class SourceSearch
    {
        public int? MediaType { get; set; }

        public int pageIndex { get; set; }
        public int pageSize => 1;
    }
}

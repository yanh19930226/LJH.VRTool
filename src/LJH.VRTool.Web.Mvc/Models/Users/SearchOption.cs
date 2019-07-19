using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Models.Users
{
    public class SearchOption
    {
        public int pageSize => 3;
        public int pageIndex => 1;
        public string KeyWord { get; set; }
        public DateTime? TimeMin { get; set; }
        public DateTime? TimeMax { get; set; }
    }
}

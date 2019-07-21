using LJH.VRTool.Web.Models.Common.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Models.Users
{
    public class UserSearch:Search
    {
        public string KeyWord { get; set; }
        public DateTime? TimeMin { get; set; }
        public DateTime? TimeMax { get; set; }
    }
}

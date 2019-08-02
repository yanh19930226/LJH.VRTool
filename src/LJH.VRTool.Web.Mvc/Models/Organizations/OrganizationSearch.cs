using LJH.VRTool.Web.Models.Common.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Models.Organizations
{
    public class OrganizationSearch : Search
    {
        public long? OrganizationId { get; set; }
        public string KeyWord { get; set; }
        public DateTime? TimeMin { get; set; }
        public DateTime? TimeMax { get; set; }

    }
}

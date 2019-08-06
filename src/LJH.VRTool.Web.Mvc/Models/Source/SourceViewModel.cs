using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdiyer.AspNetCore;

namespace LJH.VRTool.Web.Models.Source
{
    public class SourceViewModel
    {
        public int MediaType { get; set; }
        public PagedList<Source> sourcelist { get; set; }
    }

    public class Source
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string CreateTime { get; set; }
    }
}

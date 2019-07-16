using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Models.Test
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeName { get; set; }
    }
}

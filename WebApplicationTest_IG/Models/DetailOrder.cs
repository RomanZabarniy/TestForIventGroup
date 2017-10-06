using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest_IG.Models
{
    public class DetailOrder
    {
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Sum { get; set; }
        public string OrderDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest_IG.Models
{
    public class OrderForTable
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public string Date { get; set; }
        public Double SumByOrder { get; set; }
    }
}
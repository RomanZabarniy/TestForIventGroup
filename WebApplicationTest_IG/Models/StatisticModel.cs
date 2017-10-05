using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest_IG.Models
{
    public class StatisticModel
    {
        public string Category { get; set; }
        public int ClientsByCatQuantity {get; set; }
        public double SumByClientOrders { get; set; }
    }
}
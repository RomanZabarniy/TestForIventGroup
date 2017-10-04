using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest_IG.Models
{
    public class ClientModel
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        //public string Category { get; set; }
        public double SumOrders { get; set; }
    }
}
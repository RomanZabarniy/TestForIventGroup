using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest_IG.Models
{
    public class OrderRow
    {
       // [Key]
        public Guid OrderRowId { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Sum { get; set; }
    }
}
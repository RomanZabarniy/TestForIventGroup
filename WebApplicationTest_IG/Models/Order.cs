using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationTest_IG.Models
{
    public class Order
    {
        //[Key]
        public int OrderId { get; set; }
        public int ClientId { get; set; }
       // [Column("Date",TypeName = "datetime2")]
        public DateTime Date { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationTest_IG.Models
{
    public class Order
    {
        //[Key]
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
       // [Column("Date",TypeName = "datetimee2")]
        public DateTime Date { get; set; }
    }
}
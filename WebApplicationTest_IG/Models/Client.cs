using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest_IG.Models
{
    public class Client
    {
        //[Key]
        public Guid ClientId { get; set; }
      //  [MaxLength(50)]
        public string Name { get; set; }
        public string Adress { get; set; }
        public int Category { get; set; }
    }
}
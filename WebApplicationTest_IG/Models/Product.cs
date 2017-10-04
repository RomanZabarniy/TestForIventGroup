using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest_IG.Models
{
    public class Product
    {
        //[Key]
        public int ProductId { get; set; }
        //[MaxLength(50)]
        public string Name { get; set; }
    }
}
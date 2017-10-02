using System.Data.Entity;

namespace WebApplicationTest_IG.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
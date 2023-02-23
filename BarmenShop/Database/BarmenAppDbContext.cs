using BarmenShop.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BarmenShop.Database
{
    public class BarmenAppDbContext : IdentityDbContext<User>
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public BarmenAppDbContext(DbContextOptions<BarmenAppDbContext> options) : base(options)
        {

        }
    }
}

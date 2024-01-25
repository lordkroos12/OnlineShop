using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopUI.Models;

namespace OnlineShopUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CartInformation>  CartInformations { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Order>  Orders  { get; set; }
        public DbSet<OrderInformation>  OrderInformations  { get; set; }
        public DbSet<OrderStatus>  OrderStatuses  { get; set; }
        public DbSet<Part>  Parts { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}

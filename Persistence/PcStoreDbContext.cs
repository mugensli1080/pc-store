using Microsoft.EntityFrameworkCore;
using pc_store.Core.Models;

namespace pc_store.Persistence
{
    public class PcStoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<SpecificationPhoto> SpecificationPhotos { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public PcStoreDbContext(DbContextOptions<PcStoreDbContext> options) : base(options) { }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Product.WebApi.Domain.DataAccess
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {
        }

        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("server = EZE3-LLN-B02625; Database = Products; UID = sa; Password = GlobalG2019;");
            //optionsBuilder.UseSqlServer("server = EZE3-LLN-B02625; Database = Products; Trusted_Connection = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Product>(product =>
            {
                product.ToTable("Product");
                product.HasKey("Id");
                product.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}

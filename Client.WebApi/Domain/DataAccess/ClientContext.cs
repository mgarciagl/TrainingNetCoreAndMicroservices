using Microsoft.EntityFrameworkCore;

namespace Client.WebApi.Domain.DataAccess
{
    public class ClientContext : DbContext
    {
        public ClientContext()
        {
        }

        public ClientContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("server = EZE3-LLN-B02625; Database = Clients; UID = sa; Password = GlobalG2019;");
            //optionsBuilder.UseSqlServer("server = EZE3-LLN-B02625; Database = Clients; Trusted_Connection = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Client>(client =>
            {
                client.ToTable("Client");
                client.HasKey("Id");
                client.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}

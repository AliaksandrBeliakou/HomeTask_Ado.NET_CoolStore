using ADO.NET.Fundamentals.Store.EntityFramworkReposies.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace ADO.NET.Fundamentals.Store.EntityFramworkReposies.Library
{
    public class CoolStoreContext : DbContext
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Database=CoolStore;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CoolStoreContext(string connectionString) : base()
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(256)
                    .IsUnicode(false);
                entity.Property(e => e.Weight).IsRequired();
                entity.Property(e => e.Height).IsRequired();
                entity.Property(e => e.Width).IsRequired();
                entity.Property(e => e.Length).IsRequired();

            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.ProductId).IsRequired();
            });
        }
    }
}

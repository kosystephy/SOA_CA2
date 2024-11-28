using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet properties for tables
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary Keys
            modelBuilder.Entity<Categories>()
                .HasKey(c => c.Category_Id);
            modelBuilder.Entity<Customers>()
                .HasKey(c => c.Customer_Id);
            modelBuilder.Entity<Orders>()
                .HasKey(o => o.Order_Id);
            modelBuilder.Entity<OrderItems>()
                .HasKey(oi => oi.Item_Id);
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Product_Id);

            // Auto-Increment for Primary Keys
            modelBuilder.Entity<Categories>()
                .Property(c => c.Category_Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Customers>()
                .Property(c => c.Customer_Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Orders>()
                .Property(o => o.Order_Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<OrderItems>()
                .Property(oi => oi.Item_Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>()
                .Property(p => p.Product_Id)
                .ValueGeneratedOnAdd();

            // Required Fields
            modelBuilder.Entity<Categories>()
                .Property(c => c.CategoryName)
                .IsRequired();
            modelBuilder.Entity<Customers>()
                .Property(c => c.Email)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Product_Name)
                .IsRequired();
            modelBuilder.Entity<Orders>()
                .Property(o => o.Status)
                .IsRequired();

            // Relationships
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.Customer_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.Order_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.Category_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

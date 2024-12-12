using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Enums;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Logic to implement Primary Keys and Auto-Increment
            modelBuilder.Entity<User>().HasKey(u => u.User_Id);
            modelBuilder.Entity<User>().Property(u => u.User_Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>().HasKey(c => c.Category_Id);
            modelBuilder.Entity<Category>().Property(c => c.Category_Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>().HasKey(p => p.Product_Id);
            modelBuilder.Entity<Product>().Property(p => p.Product_Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Cart>().HasKey(c => c.Cart_Id);
            modelBuilder.Entity<Cart>().Property(c => c.Cart_Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<CartItem>().HasKey(ci => ci.CartItem_Id);
            modelBuilder.Entity<CartItem>().Property(ci => ci.CartItem_Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>().HasKey(o => o.Order_Id);
            modelBuilder.Entity<Order>().Property(o => o.Order_Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.OrderItem_Id);
            modelBuilder.Entity<OrderItem>().Property(oi => oi.OrderItem_Id).ValueGeneratedOnAdd();

            // **Unique Constraints**
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.ApiKey).IsUnique();

            modelBuilder.Entity<Category>().HasIndex(c => c.CategoryName).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => new { p.Product_Name, p.Category_Id }).IsUnique();

            // **Relationships mainly one to many between the tables
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.Category_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.Cart_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.Product_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.Order_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // **Default Values**
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue(UserRole.Customer);

            modelBuilder.Entity<User>()
                .Property(u => u.ApiKeyExpiration)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .Property(u => u.RefreshToken)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .Property(u => u.RefreshTokenExpiration)
                .IsRequired(false);

            //Enum-to-String Conversion for certain fields
            modelBuilder.Entity<Product>()
                .Property(p => p.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();

            // list of Optional Fields
            modelBuilder.Entity<Product>()
                .Property(p => p.ImageUrl)
                .IsRequired(false);
        }
    }
}

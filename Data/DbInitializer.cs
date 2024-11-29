using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Enums;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Seed Categories
            if (!context.Categories.Any())
            {
                Console.WriteLine("Seeding Categories...");
                context.Categories.AddRange(
                    new Categories { CategoryName = "Electronics" },
                    new Categories { CategoryName = "Clothing" },
                    new Categories { CategoryName = "Books" }
                );
                context.SaveChanges();
            }

            // Seed Products
            if (!context.Products.Any())
            {
                Console.WriteLine("Seeding Products...");
                context.Products.AddRange(
                    new Products
                    {
                        Product_Name = "Smartphone",
                        Brand = "Samsung",
                        Gender = GenderType.Unisex,
                        Stock = 50,
                        Year = 2023,
                        Description = "Latest Samsung smartphone.",
                        Image = "smartphone.jpg",
                        Price = 1200.50M,
                        Category_Id = context.Categories.First(c => c.CategoryName == "Electronics").Category_Id
                    },
                    new Products
                    {
                        Product_Name = "T-Shirt",
                        Brand = "Nike",
                        Gender = GenderType.Male,
                        Stock = 100,
                        Year = 2022,
                        Description = "Comfortable cotton T-shirt.",
                        Image = "tshirt.jpg",
                        Price = 150.00M,
                        Category_Id = context.Categories.First(c => c.CategoryName == "Clothing").Category_Id
                    }
                );
                context.SaveChanges();
            }

            // Seed Customers
            if (!context.Customers.Any())
            {
                Console.WriteLine("Seeding Customers...");
                context.Customers.AddRange(
                    new Customers
                    {
                        First_Name = "John",
                        Last_Name = "Doe",
                        Email = "john.doe@example.com",
                        PasswordHash = "hashed_password",
                        Salt = "random_salt",
                        Role = UserRole.Customer,
                        Address = "123 Main Street",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Customers
                    {
                        First_Name = "Jane",
                        Last_Name = "Smith",
                        Email = "jane.smith@example.com",
                        PasswordHash = "hashed_password",
                        Salt = "random_salt",
                        Role = UserRole.Customer,
                        Address = "456 Elm Street",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
                context.SaveChanges();
            }

            // Seed Orders
            if (!context.Orders.Any())
            {
                Console.WriteLine("Seeding Orders...");
                context.Orders.AddRange(
                    new Orders
                    {
                        Customer_Id = context.Customers.First(c => c.Email == "john.doe@example.com").Customer_Id,
                        Order_Date = DateTime.UtcNow,
                        Total_Amount = 1200.50M,
                        Status = OrderStatus.Pending
                    },
                    new Orders
                    {
                        Customer_Id = context.Customers.First(c => c.Email == "jane.smith@example.com").Customer_Id,
                        Order_Date = DateTime.UtcNow,
                        Total_Amount = 450.00M,
                        Status = OrderStatus.Completed
                    }
                );
                context.SaveChanges();
            }

            // Seed Order Items
            if (!context.OrderItems.Any())
            {
                Console.WriteLine("Seeding Order Items...");
                context.OrderItems.AddRange(
                    new OrderItems
                    {
                        Order_Id = context.Orders.First(o => o.Total_Amount == 1200.50M).Order_Id,
                        Product_Id = context.Products.First(p => p.Product_Name == "Smartphone").Product_Id,
                        Quantity = 1,
                        Price = 1200.50M
                    },
                    new OrderItems
                    {
                        Order_Id = context.Orders.First(o => o.Total_Amount == 450.00M).Order_Id,
                        Product_Id = context.Products.First(p => p.Product_Name == "T-Shirt").Product_Id,
                        Quantity = 3,
                        Price = 150.00M
                    }
                );
                context.SaveChanges();
            }

            if (!context.Auths.Any())
            {
                Console.WriteLine("Seeding Auths...");
                context.Auths.AddRange(
                    new Auths
                    {
                        Customer_Id = context.Customers.First(c => c.Email == "john.doe@example.com").Customer_Id,
                        Api_Key = Guid.NewGuid().ToString(), // Generate unique API key
                        CreatedAt = DateTime.UtcNow,
                        Expiration = DateTime.UtcNow.AddMonths(6) // API key expires in 6 months
                    },
                    new Auths
                    {
                        Customer_Id = context.Customers.First(c => c.Email == "jane.smith@example.com").Customer_Id,
                        Api_Key = Guid.NewGuid().ToString(), // Generate unique API key
                        CreatedAt = DateTime.UtcNow,
                        Expiration = DateTime.UtcNow.AddMonths(6)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

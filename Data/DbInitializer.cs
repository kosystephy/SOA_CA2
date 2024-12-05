using SOA_CA2_E_Commerce.Models;
using SOA_CA2_E_Commerce.Enums;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Helpers;

namespace SOA_CA2_E_Commerce.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Ensure database creation
            context.Database.EnsureCreated();

            // Seed Users
            if (!context.Users.Any())
            {
                var saltAdmin = PasswordHelper.GenerateSalt();
                var saltJohn = PasswordHelper.GenerateSalt();
                var saltJane = PasswordHelper.GenerateSalt();

                var hashedPasswordAdmin = PasswordHelper.HashPassword("Admin123", saltAdmin);
                var hashedPasswordJohn = PasswordHelper.HashPassword("John123", saltJohn);
                var hashedPasswordJane = PasswordHelper.HashPassword("Jane123", saltJane);

                var apiKeyAdmin = ApiKeyHelper.GenerateApiKey();
                var apiKeyJohn = ApiKeyHelper.GenerateApiKey();
                var apiKeyJane = ApiKeyHelper.GenerateApiKey();

                context.Users.AddRange(
                    new User
                    {
                        First_Name = "Admin",
                        Last_Name = "User",
                        Email = "admin@example.com",
                        PasswordHash = hashedPasswordAdmin,
                        Salt = saltAdmin,
                        Role = UserRole.Admin,
                        Address = "Admin Street",
                        ApiKey = apiKeyAdmin,
                        ApiKeyExpiration = DateTime.UtcNow.AddMonths(6),
                        RefreshToken = Guid.NewGuid().ToString(),
                        RefreshTokenExpiration = DateTime.UtcNow.AddDays(7),
                        CreatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        First_Name = "John",
                        Last_Name = "Doe",
                        Email = "john.doe@example.com",
                        PasswordHash = hashedPasswordJohn,
                        Salt = saltJohn,
                        Role = UserRole.Customer,
                        Address = "123 Elm Street",
                        ApiKey = apiKeyJohn,
                        ApiKeyExpiration = DateTime.UtcNow.AddMonths(6),
                        RefreshToken = Guid.NewGuid().ToString(),
                        RefreshTokenExpiration = DateTime.UtcNow.AddDays(7),
                        CreatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        First_Name = "Jane",
                        Last_Name = "Smith",
                        Email = "jane.smith@example.com",
                        PasswordHash = hashedPasswordJane,
                        Salt = saltJane,
                        Role = UserRole.Customer,
                        Address = "456 Pine Street",
                        ApiKey = apiKeyJane,
                        ApiKeyExpiration = DateTime.UtcNow.AddMonths(6),
                        RefreshToken = Guid.NewGuid().ToString(),
                        RefreshTokenExpiration = DateTime.UtcNow.AddDays(7),
                        CreatedAt = DateTime.UtcNow
                    }
                );
                context.SaveChanges();
            }

            // Seed Categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { CategoryName = "Clothing" },
                    new Category { CategoryName = "Shoes" },
                    new Category { CategoryName = "Accessories" }
                );
                context.SaveChanges();
            }

            // Seed Products
            if (!context.Products.Any())
            {
                var clothingCategory = context.Categories.First(c => c.CategoryName == "Clothing");
                var shoesCategory = context.Categories.First(c => c.CategoryName == "Shoes");
                context.Products.AddRange(
                    new Product
                    {
                        Product_Name = "T-Shirt",
                        Description = "A comfortable cotton t-shirt.",
                        Price = 19.99M,
                        Stock = 50,
                        Gender = GenderType.Unisex,
                        ImageUrl = "https://example.com/images/tshirt.jpg",
                        Category_Id = clothingCategory.Category_Id
                    },
                    new Product
                    {
                        Product_Name = "Jeans",
                        Description = "Stylish denim jeans.",
                        Price = 49.99M,
                        Stock = 30,
                        Gender = GenderType.Male,
                        ImageUrl = "https://example.com/images/jeans.jpg",
                        Category_Id = clothingCategory.Category_Id
                    },
                    new Product
                    {
                        Product_Name = "Sneakers",
                        Description = "Comfortable running shoes.",
                        Price = 59.99M,
                        Stock = 20,
                        Gender = GenderType.Unisex,
                        ImageUrl = "https://example.com/images/sneakers.jpg",
                        Category_Id = shoesCategory.Category_Id
                    }
                );
                context.SaveChanges();
            }

            // Seed Carts
            if (!context.Carts.Any())
            {
                var userJohn = context.Users.First(u => u.Email == "john.doe@example.com");
                context.Carts.Add(
                    new Cart
                    {
                        User_Id = userJohn.User_Id,
                        CreatedAt = DateTime.UtcNow
                    }
                );
                context.SaveChanges();
            }

            // Seed Cart Items
            if (!context.CartItems.Any())
            {
                var cart = context.Carts.First();
                var tShirt = context.Products.First(p => p.Product_Name == "T-Shirt");

                context.CartItems.Add(
                    new CartItem
                    {
                        Cart_Id = cart.Cart_Id,
                        Product_Id = tShirt.Product_Id,
                        Quantity = 2
                    }
                );
                context.SaveChanges();
            }

            // Seed Orders
            if (!context.Orders.Any())
            {
                var userJane = context.Users.First(u => u.Email == "jane.smith@example.com");
                var order = new Order
                {
                    User_Id = userJane.User_Id,
                    Order_Date = DateTime.UtcNow,
                    Total_Amount = 99.98M,
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };
                context.Orders.Add(order);
                context.SaveChanges();

                // Seed Order Items
                var jeans = context.Products.First(p => p.Product_Name == "Jeans");
                context.OrderItems.Add(
                    new OrderItem
                    {
                        Order_Id = order.Order_Id,
                        Product_Id = jeans.Product_Id,
                        Quantity = 2,
                        Price = 49.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
    }


﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SOA_CA2_E_Commerce.Data;

#nullable disable

namespace SOA_CA2_E_Commerce.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241129181811_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.Categories", b =>
                {
                    b.Property<int>("Category_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Category_Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Category_Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.Customers", b =>
                {
                    b.Property<int>("Customer_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Customer_Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Customer_Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.OrderItems", b =>
                {
                    b.Property<int>("Item_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Item_Id"));

                    b.Property<int>("Order_Id")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Product_Id")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Item_Id");

                    b.HasIndex("Order_Id");

                    b.HasIndex("Product_Id");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.Orders", b =>
                {
                    b.Property<int>("Order_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Order_Id"));

                    b.Property<int>("Customer_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Order_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Payment_Method")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Total_Amount")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Order_Id");

                    b.HasIndex("Customer_Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.Product", b =>
                {
                    b.Property<int>("Product_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Product_Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Product_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Product_Id");

                    b.HasIndex("Category_Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.OrderItems", b =>
                {
                    b.HasOne("SOA_CA2_E_Commerce.Models.Orders", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("Order_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOA_CA2_E_Commerce.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.Orders", b =>
                {
                    b.HasOne("SOA_CA2_E_Commerce.Models.Customers", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("Customer_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.Product", b =>
                {
                    b.HasOne("SOA_CA2_E_Commerce.Models.Categories", "Category")
                        .WithMany("Products")
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.Categories", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.Customers", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("SOA_CA2_E_Commerce.Models.Orders", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
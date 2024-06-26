﻿// <auto-generated />
using System;
using CoffeeStoreApplication.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeeStoreApplication.Migrations
{
    [DbContext(typeof(CoffeeStoreContext))]
    [Migration("20240525135533_UpdatedModels")]
    partial class UpdatedModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoffeeStoreApplication.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("LoyaltyPoints")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHashKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 101,
                            DateOfBirth = new DateTime(2000, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "andrew@gmail.com",
                            HashedPassword = new byte[] { 224, 73, 203, 62, 7, 233, 221, 19, 210, 69, 42, 23, 161, 212, 183, 97, 27, 187, 186, 101, 72, 46, 126, 172, 218, 247, 18, 71, 57, 249, 63, 108, 113, 155, 131, 5, 31, 67, 185, 11, 217, 152, 204, 95, 41, 141, 168, 88, 225, 227, 176, 61, 44, 89, 125, 125, 221, 160, 14, 63, 238, 188, 26, 2 },
                            LoyaltyPoints = 0,
                            Name = "Andrew",
                            PasswordHashKey = new byte[] { 246, 64, 210, 199, 244, 149, 196, 22, 246, 215, 255, 204, 235, 51, 101, 235, 132, 247, 75, 193, 89, 22, 137, 24, 253, 56, 91, 38, 95, 239, 248, 236, 11, 182, 230, 147, 205, 176, 179, 74, 127, 125, 2, 21, 25, 108, 246, 3, 36, 57, 215, 75, 255, 251, 81, 179, 98, 3, 31, 20, 186, 227, 178, 218, 51, 136, 237, 138, 198, 122, 159, 31, 39, 23, 111, 41, 18, 234, 217, 202, 232, 21, 78, 30, 122, 242, 215, 12, 231, 203, 101, 102, 28, 57, 77, 42, 104, 96, 33, 128, 4, 98, 82, 139, 119, 185, 5, 203, 196, 89, 134, 211, 223, 192, 145, 80, 129, 230, 48, 251, 70, 145, 5, 203, 120, 197, 221, 243 },
                            Phone = "9891278439"
                        });
                });

            modelBuilder.Entity("CoffeeStoreApplication.Models.CustomerOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("CustomerOrders");
                });

            modelBuilder.Entity("CoffeeStoreApplication.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHashKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<float?>("Salary")
                        .HasColumnType("real");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 201,
                            DateOfBirth = new DateTime(1997, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "abby@gmail.com",
                            HashedPassword = new byte[] { 75, 150, 191, 95, 206, 15, 80, 15, 227, 161, 201, 142, 222, 235, 98, 188, 99, 100, 129, 151, 150, 66, 35, 130, 89, 20, 53, 169, 42, 229, 107, 114, 151, 225, 148, 189, 8, 142, 30, 234, 203, 249, 91, 25, 34, 91, 121, 243, 158, 35, 94, 225, 160, 162, 218, 93, 196, 103, 22, 110, 65, 155, 135, 184 },
                            Name = "Abby",
                            PasswordHashKey = new byte[] { 246, 64, 210, 199, 244, 149, 196, 22, 246, 215, 255, 204, 235, 51, 101, 235, 132, 247, 75, 193, 89, 22, 137, 24, 253, 56, 91, 38, 95, 239, 248, 236, 11, 182, 230, 147, 205, 176, 179, 74, 127, 125, 2, 21, 25, 108, 246, 3, 36, 57, 215, 75, 255, 251, 81, 179, 98, 3, 31, 20, 186, 227, 178, 218, 51, 136, 237, 138, 198, 122, 159, 31, 39, 23, 111, 41, 18, 234, 217, 202, 232, 21, 78, 30, 122, 242, 215, 12, 231, 203, 101, 102, 28, 57, 77, 42, 104, 96, 33, 128, 4, 98, 82, 139, 119, 185, 5, 203, 196, 89, 134, 211, 223, 192, 145, 80, 129, 230, 48, 251, 70, 145, 5, 203, 120, 197, 221, 243 },
                            Phone = "9876543298",
                            Role = 0,
                            Salary = 60000f,
                            Status = 1
                        },
                        new
                        {
                            Id = 202,
                            DateOfBirth = new DateTime(1995, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "david@gmail.com",
                            HashedPassword = new byte[] { 198, 216, 138, 247, 93, 134, 160, 105, 182, 108, 124, 14, 114, 75, 172, 70, 238, 182, 6, 184, 54, 132, 191, 125, 9, 34, 246, 255, 108, 132, 47, 59, 98, 76, 231, 35, 202, 8, 99, 74, 44, 208, 10, 180, 121, 202, 124, 221, 186, 241, 132, 193, 129, 41, 237, 38, 212, 197, 73, 229, 57, 185, 75, 206 },
                            Name = "David",
                            PasswordHashKey = new byte[] { 246, 64, 210, 199, 244, 149, 196, 22, 246, 215, 255, 204, 235, 51, 101, 235, 132, 247, 75, 193, 89, 22, 137, 24, 253, 56, 91, 38, 95, 239, 248, 236, 11, 182, 230, 147, 205, 176, 179, 74, 127, 125, 2, 21, 25, 108, 246, 3, 36, 57, 215, 75, 255, 251, 81, 179, 98, 3, 31, 20, 186, 227, 178, 218, 51, 136, 237, 138, 198, 122, 159, 31, 39, 23, 111, 41, 18, 234, 217, 202, 232, 21, 78, 30, 122, 242, 215, 12, 231, 203, 101, 102, 28, 57, 77, 42, 104, 96, 33, 128, 4, 98, 82, 139, 119, 185, 5, 203, 196, 89, 134, 211, 223, 192, 145, 80, 129, 230, 48, 251, 70, 145, 5, 203, 120, 197, 221, 243 },
                            Phone = "9988776655",
                            Role = 0,
                            Salary = 80000f,
                            Status = 1
                        });
                });

            modelBuilder.Entity("CoffeeStoreApplication.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<float?>("LoyaltyPointsDiscount")
                        .HasColumnType("real");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.Property<bool>("UseLoyaltyPoints")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CoffeeStoreApplication.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("CoffeeStoreApplication.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 301,
                            Category = 0,
                            Description = "A strong, full-bodied coffee made with finely ground coffee beans and brewed under high pressure. Perfect for a quick energy boost.",
                            Name = "Espresso",
                            Price = 100f,
                            Status = 1,
                            Stock = 50
                        },
                        new
                        {
                            Id = 302,
                            Category = 0,
                            Description = "A classic Italian coffee drink made with equal parts espresso, steamed milk, and foamed milk. Smooth and creamy with a rich flavor.",
                            Name = "Cappuccino",
                            Price = 130f,
                            Status = 1,
                            Stock = 50
                        },
                        new
                        {
                            Id = 303,
                            Category = 1,
                            Description = "A refreshing iced coffee made by combining rich espresso with cold water and ice. A perfect drink for coffee lovers to enjoy on a hot day.",
                            Name = "Iced Americano",
                            Price = 110f,
                            Status = 1,
                            Stock = 50
                        },
                        new
                        {
                            Id = 304,
                            Category = 2,
                            Description = "A delicious sandwich filled with fresh vegetables, cheese, and a hint of seasoning. Ideal for a quick and healthy snack.",
                            Name = "Veg Sandwich",
                            Price = 120f,
                            Status = 1,
                            Stock = 50
                        });
                });

            modelBuilder.Entity("CoffeeStoreApplication.Models.CustomerOrder", b =>
                {
                    b.HasOne("CoffeeStoreApplication.Models.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeStoreApplication.Models.Order", "order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("order");
                });

            modelBuilder.Entity("CoffeeStoreApplication.Models.OrderItem", b =>
                {
                    b.HasOne("CoffeeStoreApplication.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoffeeStoreApplication.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CoffeeStoreApplication.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using CoffeeStoreApplication.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeeStoreApplication.Migrations
{
    [DbContext(typeof(CoffeeStoreContext))]
    partial class CoffeeStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            HashedPassword = new byte[] { 66, 204, 100, 179, 56, 127, 122, 129, 50, 61, 164, 55, 144, 44, 188, 164, 93, 173, 247, 127, 36, 115, 169, 71, 221, 85, 74, 186, 144, 170, 76, 33, 36, 126, 99, 177, 167, 180, 144, 59, 162, 227, 85, 87, 9, 3, 134, 11, 106, 26, 107, 93, 167, 142, 19, 183, 95, 74, 81, 71, 11, 20, 202, 3 },
                            LoyaltyPoints = 0,
                            Name = "Andrew",
                            PasswordHashKey = new byte[] { 0, 210, 30, 160, 203, 44, 120, 170, 96, 167, 187, 66, 83, 43, 47, 218, 80, 221, 243, 56, 255, 90, 170, 181, 28, 85, 81, 147, 239, 220, 191, 255, 211, 39, 19, 77, 183, 215, 52, 126, 20, 107, 238, 202, 246, 109, 230, 250, 248, 65, 201, 54, 241, 182, 64, 157, 206, 123, 40, 131, 25, 17, 173, 75, 56, 172, 240, 23, 35, 175, 22, 220, 240, 133, 246, 225, 140, 162, 187, 251, 81, 208, 140, 61, 48, 189, 24, 28, 199, 166, 59, 66, 157, 194, 201, 178, 105, 211, 74, 158, 85, 188, 173, 56, 1, 31, 104, 14, 115, 21, 244, 231, 83, 115, 46, 156, 227, 29, 123, 206, 101, 97, 167, 202, 37, 84, 192, 109 },
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

                    b.HasIndex("OrderId")
                        .IsUnique();

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
                            HashedPassword = new byte[] { 78, 122, 203, 166, 144, 140, 56, 149, 229, 134, 211, 168, 49, 173, 4, 144, 183, 148, 122, 101, 215, 198, 165, 161, 125, 203, 135, 12, 111, 152, 235, 161, 115, 207, 76, 101, 110, 163, 71, 131, 54, 27, 110, 150, 91, 67, 226, 165, 53, 181, 166, 6, 93, 171, 226, 219, 215, 76, 63, 84, 106, 54, 187, 235 },
                            Name = "Abby",
                            PasswordHashKey = new byte[] { 0, 210, 30, 160, 203, 44, 120, 170, 96, 167, 187, 66, 83, 43, 47, 218, 80, 221, 243, 56, 255, 90, 170, 181, 28, 85, 81, 147, 239, 220, 191, 255, 211, 39, 19, 77, 183, 215, 52, 126, 20, 107, 238, 202, 246, 109, 230, 250, 248, 65, 201, 54, 241, 182, 64, 157, 206, 123, 40, 131, 25, 17, 173, 75, 56, 172, 240, 23, 35, 175, 22, 220, 240, 133, 246, 225, 140, 162, 187, 251, 81, 208, 140, 61, 48, 189, 24, 28, 199, 166, 59, 66, 157, 194, 201, 178, 105, 211, 74, 158, 85, 188, 173, 56, 1, 31, 104, 14, 115, 21, 244, 231, 83, 115, 46, 156, 227, 29, 123, 206, 101, 97, 167, 202, 37, 84, 192, 109 },
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
                            HashedPassword = new byte[] { 111, 193, 252, 217, 154, 166, 166, 141, 232, 40, 210, 79, 56, 78, 100, 11, 203, 19, 64, 72, 227, 144, 124, 149, 18, 2, 62, 91, 175, 202, 94, 150, 253, 164, 83, 163, 56, 227, 229, 133, 181, 94, 49, 108, 97, 77, 113, 31, 151, 237, 248, 112, 117, 25, 74, 226, 61, 72, 120, 177, 1, 12, 241, 160 },
                            Name = "David",
                            PasswordHashKey = new byte[] { 0, 210, 30, 160, 203, 44, 120, 170, 96, 167, 187, 66, 83, 43, 47, 218, 80, 221, 243, 56, 255, 90, 170, 181, 28, 85, 81, 147, 239, 220, 191, 255, 211, 39, 19, 77, 183, 215, 52, 126, 20, 107, 238, 202, 246, 109, 230, 250, 248, 65, 201, 54, 241, 182, 64, 157, 206, 123, 40, 131, 25, 17, 173, 75, 56, 172, 240, 23, 35, 175, 22, 220, 240, 133, 246, 225, 140, 162, 187, 251, 81, 208, 140, 61, 48, 189, 24, 28, 199, 166, 59, 66, 157, 194, 201, 178, 105, 211, 74, 158, 85, 188, 173, 56, 1, 31, 104, 14, 115, 21, 244, 231, 83, 115, 46, 156, 227, 29, 123, 206, 101, 97, 167, 202, 37, 84, 192, 109 },
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
                    b.HasOne("CoffeeStoreApplication.Models.Customer", "Customer")
                        .WithMany("CustomerOrders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoffeeStoreApplication.Models.Order", "Order")
                        .WithOne("CustomerOrder")
                        .HasForeignKey("CoffeeStoreApplication.Models.CustomerOrder", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Order");
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

            modelBuilder.Entity("CoffeeStoreApplication.Models.Customer", b =>
                {
                    b.Navigation("CustomerOrders");
                });

            modelBuilder.Entity("CoffeeStoreApplication.Models.Order", b =>
                {
                    b.Navigation("CustomerOrder")
                        .IsRequired();

                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}

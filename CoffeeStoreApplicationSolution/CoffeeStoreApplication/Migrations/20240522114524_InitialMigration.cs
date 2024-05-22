using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeStoreApplication.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHashKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoyaltyPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HashedPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHashKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    UseLoyaltyPoints = table.Column<bool>(type: "bit", nullable: false),
                    LoyaltyPointsDiscount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "DateOfBirth", "Email", "HashedPassword", "LoyaltyPoints", "Name", "PasswordHashKey", "Phone" },
                values: new object[] { 101, new DateTime(2000, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "andrew@gmail.com", new byte[] { 11, 103, 150, 55, 87, 140, 53, 59, 215, 45, 214, 74, 27, 165, 200, 115, 0, 69, 1, 211, 124, 104, 212, 3, 130, 226, 105, 30, 214, 207, 67, 72, 193, 174, 128, 25, 200, 149, 3, 98, 48, 243, 154, 179, 176, 119, 83, 112, 219, 222, 5, 120, 56, 45, 208, 242, 253, 26, 243, 173, 247, 23, 78, 136 }, 0, "Andrew", new byte[] { 9, 189, 88, 149, 122, 116, 222, 4, 55, 251, 47, 135, 188, 98, 248, 179, 130, 191, 9, 225, 137, 79, 2, 65, 82, 42, 143, 126, 63, 1, 50, 172, 168, 45, 3, 149, 79, 99, 242, 207, 174, 13, 30, 128, 177, 119, 63, 9, 114, 46, 250, 156, 191, 171, 228, 77, 199, 154, 48, 201, 251, 43, 98, 54, 124, 12, 121, 220, 90, 191, 253, 44, 191, 50, 129, 254, 236, 160, 249, 39, 44, 91, 244, 205, 93, 129, 97, 91, 17, 82, 0, 111, 94, 111, 37, 40, 78, 166, 131, 68, 199, 183, 154, 139, 45, 92, 249, 183, 136, 81, 157, 36, 71, 238, 45, 129, 242, 42, 66, 218, 71, 169, 156, 197, 187, 146, 69, 69 }, "9891278439" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "Email", "HashedPassword", "Name", "PasswordHashKey", "Phone", "Role", "Salary", "Status" },
                values: new object[,]
                {
                    { 201, new DateTime(1997, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "abby@gmail.com", new byte[] { 38, 52, 181, 36, 132, 111, 193, 227, 152, 110, 144, 249, 132, 48, 81, 168, 25, 239, 77, 26, 72, 231, 83, 31, 116, 209, 148, 9, 0, 217, 66, 159, 17, 141, 128, 166, 201, 86, 70, 89, 206, 142, 66, 34, 190, 68, 161, 209, 123, 42, 105, 252, 229, 101, 80, 83, 9, 234, 86, 118, 43, 63, 11, 142 }, "Abby", new byte[] { 9, 189, 88, 149, 122, 116, 222, 4, 55, 251, 47, 135, 188, 98, 248, 179, 130, 191, 9, 225, 137, 79, 2, 65, 82, 42, 143, 126, 63, 1, 50, 172, 168, 45, 3, 149, 79, 99, 242, 207, 174, 13, 30, 128, 177, 119, 63, 9, 114, 46, 250, 156, 191, 171, 228, 77, 199, 154, 48, 201, 251, 43, 98, 54, 124, 12, 121, 220, 90, 191, 253, 44, 191, 50, 129, 254, 236, 160, 249, 39, 44, 91, 244, 205, 93, 129, 97, 91, 17, 82, 0, 111, 94, 111, 37, 40, 78, 166, 131, 68, 199, 183, 154, 139, 45, 92, 249, 183, 136, 81, 157, 36, 71, 238, 45, 129, 242, 42, 66, 218, 71, 169, 156, 197, 187, 146, 69, 69 }, "9876543298", 0, 60000f, 0 },
                    { 202, new DateTime(1995, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "david@gmail.com", new byte[] { 199, 19, 184, 236, 241, 213, 178, 109, 139, 16, 138, 246, 28, 116, 163, 0, 209, 109, 187, 31, 182, 203, 227, 103, 47, 17, 231, 229, 202, 68, 95, 35, 248, 134, 149, 57, 52, 252, 147, 211, 12, 50, 194, 124, 114, 141, 49, 206, 253, 236, 108, 141, 87, 136, 217, 1, 138, 179, 190, 189, 133, 4, 31, 77 }, "David", new byte[] { 9, 189, 88, 149, 122, 116, 222, 4, 55, 251, 47, 135, 188, 98, 248, 179, 130, 191, 9, 225, 137, 79, 2, 65, 82, 42, 143, 126, 63, 1, 50, 172, 168, 45, 3, 149, 79, 99, 242, 207, 174, 13, 30, 128, 177, 119, 63, 9, 114, 46, 250, 156, 191, 171, 228, 77, 199, 154, 48, 201, 251, 43, 98, 54, 124, 12, 121, 220, 90, 191, 253, 44, 191, 50, 129, 254, 236, 160, 249, 39, 44, 91, 244, 205, 93, 129, 97, 91, 17, 82, 0, 111, 94, 111, 37, 40, 78, 166, 131, 68, 199, 183, 154, 139, 45, 92, 249, 183, 136, 81, 157, 36, 71, 238, 45, 129, 242, 42, 66, 218, 71, 169, 156, 197, 187, 146, 69, 69 }, "9988776655", 0, 80000f, 0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price", "Status", "Stock" },
                values: new object[,]
                {
                    { 301, "Hot Drinks", "A strong, full-bodied coffee made with finely ground coffee beans and brewed under high pressure. Perfect for a quick energy boost.", "Espresso", 100f, 0, 50 },
                    { 302, "Hot Drinks", "A classic Italian coffee drink made with equal parts espresso, steamed milk, and foamed milk. Smooth and creamy with a rich flavor.", "Cappuccino", 130f, 0, 50 },
                    { 303, "Cold Drinks", "A refreshing iced coffee made by combining rich espresso with cold water and ice. A perfect drink for coffee lovers to enjoy on a hot day.", "Iced Americano", 110f, 0, 50 },
                    { 304, "Snacks", "A delicious sandwich filled with fresh vegetables, cheese, and a hint of seasoning. Ideal for a quick and healthy snack.", "Veg Sandwich", 120f, 0, 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_CustomerId",
                table: "CustomerOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_OrderId",
                table: "CustomerOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

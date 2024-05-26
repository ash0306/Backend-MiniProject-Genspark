using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeStoreApplication.Migrations
{
    public partial class UpdatedModelsWithOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Orders_OrderId",
                table: "CustomerOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_OrderId",
                table: "CustomerOrders");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 66, 204, 100, 179, 56, 127, 122, 129, 50, 61, 164, 55, 144, 44, 188, 164, 93, 173, 247, 127, 36, 115, 169, 71, 221, 85, 74, 186, 144, 170, 76, 33, 36, 126, 99, 177, 167, 180, 144, 59, 162, 227, 85, 87, 9, 3, 134, 11, 106, 26, 107, 93, 167, 142, 19, 183, 95, 74, 81, 71, 11, 20, 202, 3 }, new byte[] { 0, 210, 30, 160, 203, 44, 120, 170, 96, 167, 187, 66, 83, 43, 47, 218, 80, 221, 243, 56, 255, 90, 170, 181, 28, 85, 81, 147, 239, 220, 191, 255, 211, 39, 19, 77, 183, 215, 52, 126, 20, 107, 238, 202, 246, 109, 230, 250, 248, 65, 201, 54, 241, 182, 64, 157, 206, 123, 40, 131, 25, 17, 173, 75, 56, 172, 240, 23, 35, 175, 22, 220, 240, 133, 246, 225, 140, 162, 187, 251, 81, 208, 140, 61, 48, 189, 24, 28, 199, 166, 59, 66, 157, 194, 201, 178, 105, 211, 74, 158, 85, 188, 173, 56, 1, 31, 104, 14, 115, 21, 244, 231, 83, 115, 46, 156, 227, 29, 123, 206, 101, 97, 167, 202, 37, 84, 192, 109 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 78, 122, 203, 166, 144, 140, 56, 149, 229, 134, 211, 168, 49, 173, 4, 144, 183, 148, 122, 101, 215, 198, 165, 161, 125, 203, 135, 12, 111, 152, 235, 161, 115, 207, 76, 101, 110, 163, 71, 131, 54, 27, 110, 150, 91, 67, 226, 165, 53, 181, 166, 6, 93, 171, 226, 219, 215, 76, 63, 84, 106, 54, 187, 235 }, new byte[] { 0, 210, 30, 160, 203, 44, 120, 170, 96, 167, 187, 66, 83, 43, 47, 218, 80, 221, 243, 56, 255, 90, 170, 181, 28, 85, 81, 147, 239, 220, 191, 255, 211, 39, 19, 77, 183, 215, 52, 126, 20, 107, 238, 202, 246, 109, 230, 250, 248, 65, 201, 54, 241, 182, 64, 157, 206, 123, 40, 131, 25, 17, 173, 75, 56, 172, 240, 23, 35, 175, 22, 220, 240, 133, 246, 225, 140, 162, 187, 251, 81, 208, 140, 61, 48, 189, 24, 28, 199, 166, 59, 66, 157, 194, 201, 178, 105, 211, 74, 158, 85, 188, 173, 56, 1, 31, 104, 14, 115, 21, 244, 231, 83, 115, 46, 156, 227, 29, 123, 206, 101, 97, 167, 202, 37, 84, 192, 109 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 111, 193, 252, 217, 154, 166, 166, 141, 232, 40, 210, 79, 56, 78, 100, 11, 203, 19, 64, 72, 227, 144, 124, 149, 18, 2, 62, 91, 175, 202, 94, 150, 253, 164, 83, 163, 56, 227, 229, 133, 181, 94, 49, 108, 97, 77, 113, 31, 151, 237, 248, 112, 117, 25, 74, 226, 61, 72, 120, 177, 1, 12, 241, 160 }, new byte[] { 0, 210, 30, 160, 203, 44, 120, 170, 96, 167, 187, 66, 83, 43, 47, 218, 80, 221, 243, 56, 255, 90, 170, 181, 28, 85, 81, 147, 239, 220, 191, 255, 211, 39, 19, 77, 183, 215, 52, 126, 20, 107, 238, 202, 246, 109, 230, 250, 248, 65, 201, 54, 241, 182, 64, 157, 206, 123, 40, 131, 25, 17, 173, 75, 56, 172, 240, 23, 35, 175, 22, 220, 240, 133, 246, 225, 140, 162, 187, 251, 81, 208, 140, 61, 48, 189, 24, 28, 199, 166, 59, 66, 157, 194, 201, 178, 105, 211, 74, 158, 85, 188, 173, 56, 1, 31, 104, 14, 115, 21, 244, 231, 83, 115, 46, 156, 227, 29, 123, 206, 101, 97, 167, 202, 37, 84, 192, 109 } });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_OrderId",
                table: "CustomerOrders",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Orders_OrderId",
                table: "CustomerOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Orders_OrderId",
                table: "CustomerOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_OrderId",
                table: "CustomerOrders");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 224, 73, 203, 62, 7, 233, 221, 19, 210, 69, 42, 23, 161, 212, 183, 97, 27, 187, 186, 101, 72, 46, 126, 172, 218, 247, 18, 71, 57, 249, 63, 108, 113, 155, 131, 5, 31, 67, 185, 11, 217, 152, 204, 95, 41, 141, 168, 88, 225, 227, 176, 61, 44, 89, 125, 125, 221, 160, 14, 63, 238, 188, 26, 2 }, new byte[] { 246, 64, 210, 199, 244, 149, 196, 22, 246, 215, 255, 204, 235, 51, 101, 235, 132, 247, 75, 193, 89, 22, 137, 24, 253, 56, 91, 38, 95, 239, 248, 236, 11, 182, 230, 147, 205, 176, 179, 74, 127, 125, 2, 21, 25, 108, 246, 3, 36, 57, 215, 75, 255, 251, 81, 179, 98, 3, 31, 20, 186, 227, 178, 218, 51, 136, 237, 138, 198, 122, 159, 31, 39, 23, 111, 41, 18, 234, 217, 202, 232, 21, 78, 30, 122, 242, 215, 12, 231, 203, 101, 102, 28, 57, 77, 42, 104, 96, 33, 128, 4, 98, 82, 139, 119, 185, 5, 203, 196, 89, 134, 211, 223, 192, 145, 80, 129, 230, 48, 251, 70, 145, 5, 203, 120, 197, 221, 243 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 75, 150, 191, 95, 206, 15, 80, 15, 227, 161, 201, 142, 222, 235, 98, 188, 99, 100, 129, 151, 150, 66, 35, 130, 89, 20, 53, 169, 42, 229, 107, 114, 151, 225, 148, 189, 8, 142, 30, 234, 203, 249, 91, 25, 34, 91, 121, 243, 158, 35, 94, 225, 160, 162, 218, 93, 196, 103, 22, 110, 65, 155, 135, 184 }, new byte[] { 246, 64, 210, 199, 244, 149, 196, 22, 246, 215, 255, 204, 235, 51, 101, 235, 132, 247, 75, 193, 89, 22, 137, 24, 253, 56, 91, 38, 95, 239, 248, 236, 11, 182, 230, 147, 205, 176, 179, 74, 127, 125, 2, 21, 25, 108, 246, 3, 36, 57, 215, 75, 255, 251, 81, 179, 98, 3, 31, 20, 186, 227, 178, 218, 51, 136, 237, 138, 198, 122, 159, 31, 39, 23, 111, 41, 18, 234, 217, 202, 232, 21, 78, 30, 122, 242, 215, 12, 231, 203, 101, 102, 28, 57, 77, 42, 104, 96, 33, 128, 4, 98, 82, 139, 119, 185, 5, 203, 196, 89, 134, 211, 223, 192, 145, 80, 129, 230, 48, 251, 70, 145, 5, 203, 120, 197, 221, 243 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 198, 216, 138, 247, 93, 134, 160, 105, 182, 108, 124, 14, 114, 75, 172, 70, 238, 182, 6, 184, 54, 132, 191, 125, 9, 34, 246, 255, 108, 132, 47, 59, 98, 76, 231, 35, 202, 8, 99, 74, 44, 208, 10, 180, 121, 202, 124, 221, 186, 241, 132, 193, 129, 41, 237, 38, 212, 197, 73, 229, 57, 185, 75, 206 }, new byte[] { 246, 64, 210, 199, 244, 149, 196, 22, 246, 215, 255, 204, 235, 51, 101, 235, 132, 247, 75, 193, 89, 22, 137, 24, 253, 56, 91, 38, 95, 239, 248, 236, 11, 182, 230, 147, 205, 176, 179, 74, 127, 125, 2, 21, 25, 108, 246, 3, 36, 57, 215, 75, 255, 251, 81, 179, 98, 3, 31, 20, 186, 227, 178, 218, 51, 136, 237, 138, 198, 122, 159, 31, 39, 23, 111, 41, 18, 234, 217, 202, 232, 21, 78, 30, 122, 242, 215, 12, 231, 203, 101, 102, 28, 57, 77, 42, 104, 96, 33, 128, 4, 98, 82, 139, 119, 185, 5, 203, 196, 89, 134, 211, 223, 192, 145, 80, 129, 230, 48, 251, 70, 145, 5, 203, 120, 197, 221, 243 } });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_OrderId",
                table: "CustomerOrders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Orders_OrderId",
                table: "CustomerOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

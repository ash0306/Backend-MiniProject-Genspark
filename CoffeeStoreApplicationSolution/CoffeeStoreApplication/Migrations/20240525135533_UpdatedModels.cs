using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeStoreApplication.Migrations
{
    public partial class UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "LoyaltyPointsDiscount",
                table: "Orders",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 301,
                column: "Category",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 302,
                column: "Category",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 303,
                column: "Category",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 304,
                column: "Category",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "LoyaltyPointsDiscount",
                table: "Orders",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 45, 96, 252, 141, 163, 211, 188, 99, 229, 136, 141, 119, 39, 22, 39, 85, 155, 29, 60, 33, 243, 169, 30, 29, 215, 113, 74, 67, 83, 234, 61, 60, 56, 60, 222, 102, 2, 25, 161, 192, 62, 101, 11, 173, 14, 82, 140, 144, 12, 36, 14, 129, 110, 28, 194, 86, 237, 187, 27, 156, 224, 52, 113, 3 }, new byte[] { 27, 250, 99, 117, 233, 253, 101, 139, 97, 102, 254, 134, 72, 164, 99, 208, 79, 0, 202, 34, 58, 208, 58, 0, 212, 200, 80, 11, 18, 165, 115, 49, 151, 31, 77, 173, 56, 0, 211, 35, 18, 234, 178, 41, 182, 182, 45, 252, 66, 223, 8, 145, 37, 69, 233, 163, 176, 135, 221, 190, 31, 189, 187, 187, 132, 216, 64, 247, 122, 208, 203, 136, 52, 0, 215, 245, 164, 137, 188, 20, 139, 133, 82, 39, 58, 30, 130, 99, 76, 181, 13, 16, 118, 248, 206, 190, 237, 219, 180, 153, 127, 134, 24, 166, 225, 78, 80, 48, 199, 249, 209, 62, 13, 237, 68, 50, 16, 185, 255, 221, 173, 146, 233, 62, 0, 87, 72, 200 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 210, 196, 68, 43, 179, 124, 207, 196, 186, 97, 15, 14, 252, 14, 19, 148, 69, 219, 3, 57, 248, 246, 46, 217, 5, 142, 99, 30, 174, 28, 19, 185, 140, 211, 127, 216, 233, 135, 229, 247, 59, 87, 186, 103, 66, 224, 212, 223, 190, 217, 90, 1, 177, 118, 15, 225, 223, 93, 196, 26, 75, 212, 52, 164 }, new byte[] { 27, 250, 99, 117, 233, 253, 101, 139, 97, 102, 254, 134, 72, 164, 99, 208, 79, 0, 202, 34, 58, 208, 58, 0, 212, 200, 80, 11, 18, 165, 115, 49, 151, 31, 77, 173, 56, 0, 211, 35, 18, 234, 178, 41, 182, 182, 45, 252, 66, 223, 8, 145, 37, 69, 233, 163, 176, 135, 221, 190, 31, 189, 187, 187, 132, 216, 64, 247, 122, 208, 203, 136, 52, 0, 215, 245, 164, 137, 188, 20, 139, 133, 82, 39, 58, 30, 130, 99, 76, 181, 13, 16, 118, 248, 206, 190, 237, 219, 180, 153, 127, 134, 24, 166, 225, 78, 80, 48, 199, 249, 209, 62, 13, 237, 68, 50, 16, 185, 255, 221, 173, 146, 233, 62, 0, 87, 72, 200 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 119, 186, 106, 109, 236, 179, 135, 11, 62, 235, 125, 75, 34, 82, 128, 54, 75, 100, 16, 90, 10, 54, 191, 3, 16, 73, 174, 42, 128, 148, 92, 82, 115, 14, 162, 172, 206, 102, 88, 254, 99, 122, 86, 192, 204, 249, 93, 218, 246, 103, 166, 225, 163, 223, 159, 137, 17, 250, 229, 183, 245, 204, 40, 37 }, new byte[] { 27, 250, 99, 117, 233, 253, 101, 139, 97, 102, 254, 134, 72, 164, 99, 208, 79, 0, 202, 34, 58, 208, 58, 0, 212, 200, 80, 11, 18, 165, 115, 49, 151, 31, 77, 173, 56, 0, 211, 35, 18, 234, 178, 41, 182, 182, 45, 252, 66, 223, 8, 145, 37, 69, 233, 163, 176, 135, 221, 190, 31, 189, 187, 187, 132, 216, 64, 247, 122, 208, 203, 136, 52, 0, 215, 245, 164, 137, 188, 20, 139, 133, 82, 39, 58, 30, 130, 99, 76, 181, 13, 16, 118, 248, 206, 190, 237, 219, 180, 153, 127, 134, 24, 166, 225, 78, 80, 48, 199, 249, 209, 62, 13, 237, 68, 50, 16, 185, 255, 221, 173, 146, 233, 62, 0, 87, 72, 200 } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 301,
                column: "Category",
                value: "Hot Drinks");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 302,
                column: "Category",
                value: "Hot Drinks");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 303,
                column: "Category",
                value: "Cold Drinks");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 304,
                column: "Category",
                value: "Snacks");
        }
    }
}

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeStoreApplication.Migrations
{
    public partial class UpdatedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 27, 226, 177, 168, 101, 104, 223, 216, 175, 212, 15, 118, 148, 112, 145, 13, 55, 222, 203, 240, 125, 15, 196, 246, 110, 166, 216, 221, 12, 106, 209, 30, 241, 54, 78, 138, 191, 31, 41, 116, 57, 95, 77, 175, 74, 0, 170, 109, 6, 66, 57, 28, 221, 42, 199, 253, 57, 181, 155, 84, 2, 104, 71, 36 }, new byte[] { 115, 34, 117, 158, 170, 23, 32, 46, 217, 196, 98, 115, 196, 197, 209, 128, 216, 204, 31, 22, 247, 7, 207, 233, 208, 186, 254, 1, 126, 37, 41, 148, 222, 195, 234, 137, 120, 249, 169, 28, 182, 90, 56, 2, 193, 201, 233, 56, 198, 232, 177, 234, 89, 178, 169, 20, 209, 56, 247, 202, 244, 24, 73, 66, 220, 16, 171, 235, 164, 152, 157, 198, 184, 160, 182, 255, 21, 231, 79, 41, 209, 155, 143, 35, 16, 235, 120, 56, 118, 232, 89, 32, 102, 79, 88, 159, 186, 206, 176, 72, 86, 43, 55, 162, 36, 187, 202, 55, 224, 102, 223, 78, 3, 107, 80, 164, 98, 190, 231, 215, 100, 44, 69, 126, 72, 32, 44, 175 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 86, 198, 95, 88, 88, 162, 172, 96, 238, 70, 56, 37, 35, 2, 209, 209, 43, 223, 86, 144, 249, 173, 3, 100, 15, 228, 109, 214, 204, 218, 173, 193, 103, 43, 120, 72, 136, 220, 49, 112, 63, 235, 163, 11, 42, 189, 9, 66, 51, 126, 144, 81, 33, 179, 208, 223, 250, 247, 168, 242, 148, 44, 173, 83 }, new byte[] { 115, 34, 117, 158, 170, 23, 32, 46, 217, 196, 98, 115, 196, 197, 209, 128, 216, 204, 31, 22, 247, 7, 207, 233, 208, 186, 254, 1, 126, 37, 41, 148, 222, 195, 234, 137, 120, 249, 169, 28, 182, 90, 56, 2, 193, 201, 233, 56, 198, 232, 177, 234, 89, 178, 169, 20, 209, 56, 247, 202, 244, 24, 73, 66, 220, 16, 171, 235, 164, 152, 157, 198, 184, 160, 182, 255, 21, 231, 79, 41, 209, 155, 143, 35, 16, 235, 120, 56, 118, 232, 89, 32, 102, 79, 88, 159, 186, 206, 176, 72, 86, 43, 55, 162, 36, 187, 202, 55, 224, 102, 223, 78, 3, 107, 80, 164, 98, 190, 231, 215, 100, 44, 69, 126, 72, 32, 44, 175 } });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "HashedPassword", "PasswordHashKey" },
                values: new object[] { new byte[] { 184, 189, 106, 164, 161, 250, 247, 4, 206, 126, 233, 61, 80, 106, 108, 197, 10, 35, 70, 106, 238, 248, 131, 126, 73, 91, 138, 212, 255, 230, 176, 123, 97, 167, 140, 141, 248, 248, 173, 205, 92, 175, 230, 214, 121, 122, 225, 156, 23, 232, 158, 195, 134, 38, 153, 197, 26, 111, 143, 239, 195, 160, 3, 38 }, new byte[] { 115, 34, 117, 158, 170, 23, 32, 46, 217, 196, 98, 115, 196, 197, 209, 128, 216, 204, 31, 22, 247, 7, 207, 233, 208, 186, 254, 1, 126, 37, 41, 148, 222, 195, 234, 137, 120, 249, 169, 28, 182, 90, 56, 2, 193, 201, 233, 56, 198, 232, 177, 234, 89, 178, 169, 20, 209, 56, 247, 202, 244, 24, 73, 66, 220, 16, 171, 235, 164, 152, 157, 198, 184, 160, 182, 255, 21, 231, 79, 41, 209, 155, 143, 35, 16, 235, 120, 56, 118, 232, 89, 32, 102, 79, 88, 159, 186, 206, 176, 72, 86, 43, 55, 162, 36, 187, 202, 55, 224, 102, 223, 78, 3, 107, 80, 164, 98, 190, 231, 215, 100, 44, 69, 126, 72, 32, 44, 175 } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 301,
                column: "Image",
                value: "https://img.freepik.com/free-vector/realistic-cup-black-brewed-coffee-saucer-vector-illustration_1284-66002.jpg?t=st=1718614373~exp=1718617973~hmac=a5c17f4a2e84d0f7b60ef86b88bb2776f8d0aa34a46139ea578010a7d37517a1&w=740");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 302,
                column: "Image",
                value: "https://images.unsplash.com/photo-1602320574582-741740d4fcd7?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 303,
                column: "Image",
                value: "https://images.unsplash.com/photo-1581996323441-538096e854b9?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 304,
                column: "Image",
                value: "https://plus.unsplash.com/premium_photo-1671559021919-19d9610c8cad?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

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
        }
    }
}
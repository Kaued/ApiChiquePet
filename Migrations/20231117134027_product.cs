using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8daf0da2-bfa8-4bfa-9c1e-e891936d84ef", "5c46122f-c83f-486d-abfe-9dd3e453589d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "be008f6a-887b-43d8-9b60-c99859b15d0d", "5c46122f-c83f-486d-abfe-9dd3e453589d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d208a594-99b5-4bb8-a394-19e4cf3e82f6", "5c46122f-c83f-486d-abfe-9dd3e453589d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8daf0da2-bfa8-4bfa-9c1e-e891936d84ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be008f6a-887b-43d8-9b60-c99859b15d0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d208a594-99b5-4bb8-a394-19e4cf3e82f6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5c46122f-c83f-486d-abfe-9dd3e453589d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "347bd24d-66e5-45c7-ad6c-2b8e403352eb", null, "Super Admin", "SUPER ADMIN" },
                    { "5b52ca37-7b2f-4d94-8e76-49566c4be7bf", null, "Seller", "SELLER" },
                    { "f614194c-2cdb-44b5-8e3d-cd9fb3ba4afe", null, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2bb20f36-e3a0-44e4-b204-cdc9b77db7c1", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "c231609a-0476-4ffd-9e78-59d03f89bac6", "kauedomingues98@gmail.com", true, false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAEK6Zl0wDiRqsbdHmiC1wEiwRswsGoCnERap7NJF4n2FslmD9xOgC6f1zuLEmAUI9Dg==", "17996583206", true, "145dcb3d-3d07-4dd4-9536-03108a5aaae1", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "347bd24d-66e5-45c7-ad6c-2b8e403352eb", "2bb20f36-e3a0-44e4-b204-cdc9b77db7c1" },
                    { "5b52ca37-7b2f-4d94-8e76-49566c4be7bf", "2bb20f36-e3a0-44e4-b204-cdc9b77db7c1" },
                    { "f614194c-2cdb-44b5-8e3d-cd9fb3ba4afe", "2bb20f36-e3a0-44e4-b204-cdc9b77db7c1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "347bd24d-66e5-45c7-ad6c-2b8e403352eb", "2bb20f36-e3a0-44e4-b204-cdc9b77db7c1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5b52ca37-7b2f-4d94-8e76-49566c4be7bf", "2bb20f36-e3a0-44e4-b204-cdc9b77db7c1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f614194c-2cdb-44b5-8e3d-cd9fb3ba4afe", "2bb20f36-e3a0-44e4-b204-cdc9b77db7c1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "347bd24d-66e5-45c7-ad6c-2b8e403352eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b52ca37-7b2f-4d94-8e76-49566c4be7bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f614194c-2cdb-44b5-8e3d-cd9fb3ba4afe");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2bb20f36-e3a0-44e4-b204-cdc9b77db7c1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8daf0da2-bfa8-4bfa-9c1e-e891936d84ef", null, "Seller", "SELLER" },
                    { "be008f6a-887b-43d8-9b60-c99859b15d0d", null, "Client", "CLIENT" },
                    { "d208a594-99b5-4bb8-a394-19e4cf3e82f6", null, "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5c46122f-c83f-486d-abfe-9dd3e453589d", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "c0823874-0ca1-462f-a909-16fa12881865", "kauedomingues98@gmail.com", true, false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAEMBKpBsxvJ26iudRb0KR8gz26w7Sh3L4yjyQ7R7WF+12Z23DzAx1RTOJqrxkLF/yLA==", "17996583206", true, "e971a33d-dbb1-441f-99d1-378f4e1ef1a7", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8daf0da2-bfa8-4bfa-9c1e-e891936d84ef", "5c46122f-c83f-486d-abfe-9dd3e453589d" },
                    { "be008f6a-887b-43d8-9b60-c99859b15d0d", "5c46122f-c83f-486d-abfe-9dd3e453589d" },
                    { "d208a594-99b5-4bb8-a394-19e4cf3e82f6", "5c46122f-c83f-486d-abfe-9dd3e453589d" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class imageurl_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0bc929cb-d120-4ae1-9bb0-f302ce393b90", "c446221c-140a-4846-9cad-23859c41337f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2a672b5c-921a-4bd3-a3b1-fc549c34d9b4", "c446221c-140a-4846-9cad-23859c41337f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5709c878-7285-4ebe-a44b-62c24e20b141", "c446221c-140a-4846-9cad-23859c41337f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bc929cb-d120-4ae1-9bb0-f302ce393b90");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a672b5c-921a-4bd3-a3b1-fc549c34d9b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5709c878-7285-4ebe-a44b-62c24e20b141");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c446221c-140a-4846-9cad-23859c41337f");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ImageUrl",
                newName: "Path");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ImageUrl",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c65d55b-0340-4b87-8a83-27f8cdc7ee7f", null, "Super Admin", "SUPER ADMIN" },
                    { "72aecf26-034f-4b35-a328-10d8e07b0fc1", null, "Seller", "SELLER" },
                    { "a098cd71-927e-415f-bfbc-f4daaa936230", null, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "76814f5c-49aa-4253-9059-bd9b0ea14587", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "b603f0a8-12f4-40f6-afe2-53573b2b1564", "kauedomingues98@gmail.com", true, false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAENPhhF/Yy6MOr7NclvVcEpZBEPUUOdxayNC5Z0KB110k1dztR3vwoqjDKtDbqanrIQ==", "17996583206", true, "1569b474-845f-441d-8d86-646a415a55d4", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2c65d55b-0340-4b87-8a83-27f8cdc7ee7f", "76814f5c-49aa-4253-9059-bd9b0ea14587" },
                    { "72aecf26-034f-4b35-a328-10d8e07b0fc1", "76814f5c-49aa-4253-9059-bd9b0ea14587" },
                    { "a098cd71-927e-415f-bfbc-f4daaa936230", "76814f5c-49aa-4253-9059-bd9b0ea14587" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c65d55b-0340-4b87-8a83-27f8cdc7ee7f", "76814f5c-49aa-4253-9059-bd9b0ea14587" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "72aecf26-034f-4b35-a328-10d8e07b0fc1", "76814f5c-49aa-4253-9059-bd9b0ea14587" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a098cd71-927e-415f-bfbc-f4daaa936230", "76814f5c-49aa-4253-9059-bd9b0ea14587" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c65d55b-0340-4b87-8a83-27f8cdc7ee7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72aecf26-034f-4b35-a328-10d8e07b0fc1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a098cd71-927e-415f-bfbc-f4daaa936230");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76814f5c-49aa-4253-9059-bd9b0ea14587");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "ImageUrl",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Product",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ImageUrl",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0bc929cb-d120-4ae1-9bb0-f302ce393b90", null, "Seller", "SELLER" },
                    { "2a672b5c-921a-4bd3-a3b1-fc549c34d9b4", null, "Client", "CLIENT" },
                    { "5709c878-7285-4ebe-a44b-62c24e20b141", null, "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c446221c-140a-4846-9cad-23859c41337f", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "233240e9-5707-44cf-bac1-4f6c381af2f1", "kauedomingues98@gmail.com", true, false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAEC2NUCQiKfJ2BgShYYGZpy0nbroxxCQJ3U14GHjkFZj/kK9BMdXtI79yJoEVLbOx8A==", "17996583206", true, "631102ec-50d9-4df7-bc05-c3172e0bae35", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0bc929cb-d120-4ae1-9bb0-f302ce393b90", "c446221c-140a-4846-9cad-23859c41337f" },
                    { "2a672b5c-921a-4bd3-a3b1-fc549c34d9b4", "c446221c-140a-4846-9cad-23859c41337f" },
                    { "5709c878-7285-4ebe-a44b-62c24e20b141", "c446221c-140a-4846-9cad-23859c41337f" }
                });
        }
    }
}

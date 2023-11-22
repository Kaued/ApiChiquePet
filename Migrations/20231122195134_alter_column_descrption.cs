using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class alter_column_descrption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "775cf21c-1fd3-4c5b-b957-5f615c0e1fa8", "ef5bea51-6fe0-420c-a9f8-bb2dd3d9eecb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7d335eed-ba77-4648-ab96-fe4b12e9a479", "ef5bea51-6fe0-420c-a9f8-bb2dd3d9eecb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fed2a538-bcfe-48f3-8567-77462bd899ca", "ef5bea51-6fe0-420c-a9f8-bb2dd3d9eecb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "775cf21c-1fd3-4c5b-b957-5f615c0e1fa8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d335eed-ba77-4648-ab96-fe4b12e9a479");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fed2a538-bcfe-48f3-8567-77462bd899ca");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ef5bea51-6fe0-420c-a9f8-bb2dd3d9eecb");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32b07a9d-266d-4b61-aef2-21a1953db917", null, "Seller", "SELLER" },
                    { "377998b5-5705-42d4-bc54-38d03dbdc683", null, "Client", "CLIENT" },
                    { "f2edfd71-f869-41b3-9dc0-239495e7f72c", null, "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "893638d9-bda9-4ccd-aded-9f803767e069", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "3e844e4f-3e2c-4783-a407-7f6ddc00ce0a", "kauedomingues98@gmail.com", true, false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAEKJNC+4P7afyBpzzE0RsERTqlFmDtxPW5RyScOrwwLDcptPp+ZzlCvrguSTQDYyoYQ==", "17996583206", true, "839d1b91-ac94-4f22-89e5-ec92c87b110b", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "32b07a9d-266d-4b61-aef2-21a1953db917", "893638d9-bda9-4ccd-aded-9f803767e069" },
                    { "377998b5-5705-42d4-bc54-38d03dbdc683", "893638d9-bda9-4ccd-aded-9f803767e069" },
                    { "f2edfd71-f869-41b3-9dc0-239495e7f72c", "893638d9-bda9-4ccd-aded-9f803767e069" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "32b07a9d-266d-4b61-aef2-21a1953db917", "893638d9-bda9-4ccd-aded-9f803767e069" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "377998b5-5705-42d4-bc54-38d03dbdc683", "893638d9-bda9-4ccd-aded-9f803767e069" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f2edfd71-f869-41b3-9dc0-239495e7f72c", "893638d9-bda9-4ccd-aded-9f803767e069" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32b07a9d-266d-4b61-aef2-21a1953db917");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "377998b5-5705-42d4-bc54-38d03dbdc683");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2edfd71-f869-41b3-9dc0-239495e7f72c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "893638d9-bda9-4ccd-aded-9f803767e069");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "775cf21c-1fd3-4c5b-b957-5f615c0e1fa8", null, "Client", "CLIENT" },
                    { "7d335eed-ba77-4648-ab96-fe4b12e9a479", null, "Seller", "SELLER" },
                    { "fed2a538-bcfe-48f3-8567-77462bd899ca", null, "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ef5bea51-6fe0-420c-a9f8-bb2dd3d9eecb", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "a85a6864-ecd1-4a6b-8ac7-13493fb550ed", "kauedomingues98@gmail.com", true, false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAEGGE85t3qrlBPL7hxuBzqKKUF6oT+28wnUg0lCe86WV5DUDNhLGmsODnl82f65HFeA==", "17996583206", true, "36955ff5-cdd1-4bb2-8e2b-2b197849b62d", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "775cf21c-1fd3-4c5b-b957-5f615c0e1fa8", "ef5bea51-6fe0-420c-a9f8-bb2dd3d9eecb" },
                    { "7d335eed-ba77-4648-ab96-fe4b12e9a479", "ef5bea51-6fe0-420c-a9f8-bb2dd3d9eecb" },
                    { "fed2a538-bcfe-48f3-8567-77462bd899ca", "ef5bea51-6fe0-420c-a9f8-bb2dd3d9eecb" }
                });
        }
    }
}

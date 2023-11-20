using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class imageurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ImageUrl",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUrl", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ImageUrl_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrl_ProductId",
                table: "ImageUrl",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageUrl");

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
    }
}

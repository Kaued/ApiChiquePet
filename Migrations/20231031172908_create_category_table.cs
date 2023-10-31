using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class create_category_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_CategoriaId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "73a762d9-e514-488b-afe8-2e6ac8aa6a2f", "3e63abf9-e8a4-458b-ac64-f8b17f94f746" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "79f8db08-c92e-4760-ad3a-f26088d39af3", "3e63abf9-e8a4-458b-ac64-f8b17f94f746" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "829f3b6e-3d1d-4692-af2a-7ae11e70cb46", "3e63abf9-e8a4-458b-ac64-f8b17f94f746" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73a762d9-e514-488b-afe8-2e6ac8aa6a2f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79f8db08-c92e-4760-ad3a-f26088d39af3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "829f3b6e-3d1d-4692-af2a-7ae11e70cb46");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e63abf9-e8a4-458b-ac64-f8b17f94f746");

            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Produtos");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "decimal(7,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "Altura",
                table: "Produtos",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Largura",
                table: "Produtos",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageUrl = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18b492d6-d887-4ffd-bf31-e3c34136f648", null, "Seller", "SELLER" },
                    { "72e83531-3a4d-4a9e-9e6d-9be1acbcdbca", null, "Client", "CLIENT" },
                    { "e3138cd7-a928-43c3-afaf-2d02c90282cb", null, "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dca687be-9684-4d16-80d4-264c77839b90", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "5bcf586a-8ff3-4605-bc42-629fa9cf3ab8", "kauedomingues98@gmail.com", true, false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAEJzMUluhwMW5lTFx5VEhpyQVjqB7S+shUAOflLDggCIxdXzubruUryIjokY9Kl3eNQ==", "17996583206", true, "1e24bdb3-61a1-4ccb-a506-54d45c710434", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "18b492d6-d887-4ffd-bf31-e3c34136f648", "dca687be-9684-4d16-80d4-264c77839b90" },
                    { "72e83531-3a4d-4a9e-9e6d-9be1acbcdbca", "dca687be-9684-4d16-80d4-264c77839b90" },
                    { "e3138cd7-a928-43c3-afaf-2d02c90282cb", "dca687be-9684-4d16-80d4-264c77839b90" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categories_CategoriaId",
                table: "Produtos",
                column: "CategoriaId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categories_CategoriaId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "18b492d6-d887-4ffd-bf31-e3c34136f648", "dca687be-9684-4d16-80d4-264c77839b90" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "72e83531-3a4d-4a9e-9e6d-9be1acbcdbca", "dca687be-9684-4d16-80d4-264c77839b90" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e3138cd7-a928-43c3-afaf-2d02c90282cb", "dca687be-9684-4d16-80d4-264c77839b90" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18b492d6-d887-4ffd-bf31-e3c34136f648");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72e83531-3a4d-4a9e-9e6d-9be1acbcdbca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3138cd7-a928-43c3-afaf-2d02c90282cb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dca687be-9684-4d16-80d4-264c77839b90");

            migrationBuilder.DropColumn(
                name: "Altura",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Largura",
                table: "Produtos");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)");

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Produtos",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ImagemUrl = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73a762d9-e514-488b-afe8-2e6ac8aa6a2f", null, "Client", "CLIENT" },
                    { "79f8db08-c92e-4760-ad3a-f26088d39af3", null, "Seller", "SELLER" },
                    { "829f3b6e-3d1d-4692-af2a-7ae11e70cb46", null, "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3e63abf9-e8a4-458b-ac64-f8b17f94f746", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "d6c4cff0-9da2-4bdc-9017-89b12e9ed769", "kauedomingues98@gmail.com", true, false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAEA6rE/kh2HD5G5AgiMe4YNt0cwcx+buiknLFE3vHix+JMVKI4tjq1xcn7TxHfyCSlg==", "17996583206", true, "a4061030-8592-4b4b-a0a6-cc50f7773d9a", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "73a762d9-e514-488b-afe8-2e6ac8aa6a2f", "3e63abf9-e8a4-458b-ac64-f8b17f94f746" },
                    { "79f8db08-c92e-4760-ad3a-f26088d39af3", "3e63abf9-e8a4-458b-ac64-f8b17f94f746" },
                    { "829f3b6e-3d1d-4692-af2a-7ae11e70cb46", "3e63abf9-e8a4-458b-ac64-f8b17f94f746" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_CategoriaId",
                table: "Produtos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

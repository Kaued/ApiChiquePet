using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class alter_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

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

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Width = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Stock = table.Column<float>(type: "float", nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

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

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estoque = table.Column<float>(type: "float", nullable: false),
                    Largura = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preco = table.Column<decimal>(type: "decimal(7,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produtos_Categories_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }
    }
}

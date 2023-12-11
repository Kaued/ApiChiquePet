using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiChikPet.Migrations
{
    /// <inheritdoc />
    public partial class add_full_name_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Product_ProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4f0fcdf6-98a4-4485-a7ec-1efa2da11363", "3a21f403-56c5-49b6-b100-70cae07d63d2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "913e9ae7-f8eb-42a7-b53f-43d4faa3e50b", "3a21f403-56c5-49b6-b100-70cae07d63d2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9838fc98-ca30-4e31-b81f-e184f082c57b", "3a21f403-56c5-49b6-b100-70cae07d63d2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f0fcdf6-98a4-4485-a7ec-1efa2da11363");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "913e9ae7-f8eb-42a7-b53f-43d4faa3e50b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9838fc98-ca30-4e31-b81f-e184f082c57b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a21f403-56c5-49b6-b100-70cae07d63d2");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "varchar(123)",
                maxLength: 123,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47c75dda-c4cb-4528-90b4-8b59791a0961", null, "Client", "CLIENT" },
                    { "910b7006-ce9e-4acd-b31c-2a9e0f79b857", null, "Seller", "SELLER" },
                    { "e2e87aa9-442c-4652-a5e8-dcb134993665", null, "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "365d9dd0-8c0c-4fd2-881e-ba7b15be90c6", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "72e5f972-38ec-4496-b1e3-af2236eb1582", "kauedomingues98@gmail.com", true, "Chique Pet", false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAEL4rI+RFCcX6/Xhjx0RY5CZY0Bc2IrDdE8vtR2kqCM+PPxD9hsbirVPLtHGNocSSpA==", "17996583206", true, "30f0c1bb-6ff1-4a78-9e99-d8ef2a80f68e", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "47c75dda-c4cb-4528-90b4-8b59791a0961", "365d9dd0-8c0c-4fd2-881e-ba7b15be90c6" },
                    { "910b7006-ce9e-4acd-b31c-2a9e0f79b857", "365d9dd0-8c0c-4fd2-881e-ba7b15be90c6" },
                    { "e2e87aa9-442c-4652-a5e8-dcb134993665", "365d9dd0-8c0c-4fd2-881e-ba7b15be90c6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "47c75dda-c4cb-4528-90b4-8b59791a0961", "365d9dd0-8c0c-4fd2-881e-ba7b15be90c6" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "910b7006-ce9e-4acd-b31c-2a9e0f79b857", "365d9dd0-8c0c-4fd2-881e-ba7b15be90c6" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e2e87aa9-442c-4652-a5e8-dcb134993665", "365d9dd0-8c0c-4fd2-881e-ba7b15be90c6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47c75dda-c4cb-4528-90b4-8b59791a0961");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "910b7006-ce9e-4acd-b31c-2a9e0f79b857");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2e87aa9-442c-4652-a5e8-dcb134993665");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "365d9dd0-8c0c-4fd2-881e-ba7b15be90c6");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Orders",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f0fcdf6-98a4-4485-a7ec-1efa2da11363", null, "Client", "CLIENT" },
                    { "913e9ae7-f8eb-42a7-b53f-43d4faa3e50b", null, "Super Admin", "SUPER ADMIN" },
                    { "9838fc98-ca30-4e31-b81f-e184f082c57b", null, "Seller", "SELLER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3a21f403-56c5-49b6-b100-70cae07d63d2", 0, new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "d30575ee-241a-4acf-b792-9a0b02a8d331", "kauedomingues98@gmail.com", true, false, null, "KAUEDOMINGUES98@GMAIL.COM", "CHIQUE PET", "AQAAAAIAAYagAAAAEC3FDeOE4b3kQslUHn/35SdeDVfW1P8PCPhvDc7L3LjEFS+uA8rZ09ih/1roBD91hQ==", "17996583206", true, "fe069501-b033-4dce-bfce-6151af517d49", false, "Chique Pet" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4f0fcdf6-98a4-4485-a7ec-1efa2da11363", "3a21f403-56c5-49b6-b100-70cae07d63d2" },
                    { "913e9ae7-f8eb-42a7-b53f-43d4faa3e50b", "3a21f403-56c5-49b6-b100-70cae07d63d2" },
                    { "9838fc98-ca30-4e31-b81f-e184f082c57b", "3a21f403-56c5-49b6-b100-70cae07d63d2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Product_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Shop.Api.Migrations
{
    public partial class MigrationMySQLInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingSession",
                columns: table => new
                {
                    ShoppingSesionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingSession", x => x.ShoppingSesionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingDetail",
                columns: table => new
                {
                    ShoppingSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SelectedProduct = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShoppingSesionId = table.Column<int>(type: "int", nullable: false),
                    shoppingSessionShoppingSesionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingDetail", x => x.ShoppingSessionId);
                    table.ForeignKey(
                        name: "FK_ShoppingDetail_ShoppingSession_shoppingSessionShoppingSesion~",
                        column: x => x.shoppingSessionShoppingSesionId,
                        principalTable: "ShoppingSession",
                        principalColumn: "ShoppingSesionId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingDetail_shoppingSessionShoppingSesionId",
                table: "ShoppingDetail",
                column: "shoppingSessionShoppingSesionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingDetail");

            migrationBuilder.DropTable(
                name: "ShoppingSession");
        }
    }
}

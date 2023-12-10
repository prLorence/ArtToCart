using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtToCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdersInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerId = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ShipToAddress_Street = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    ShipToAddress_City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ShipToAddress_State = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    ShipToAddress_Country = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false),
                    ShipToAddress_ZipCode = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemOrdered_CatalogItemId = table.Column<int>(type: "integer", nullable: false),
                    ItemOrdered_ProductName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ItemOrdered_PictureUri = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Units = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}

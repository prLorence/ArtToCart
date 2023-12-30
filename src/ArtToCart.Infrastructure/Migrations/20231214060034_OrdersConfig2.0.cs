using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtToCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdersConfig20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipToAddress_State",
                table: "Order",
                newName: "ShipToAddress_Province");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipToAddress_Province",
                table: "Order",
                newName: "ShipToAddress_State");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtToCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BasketItemSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "BasketItem",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "BasketItem");
        }
    }
}

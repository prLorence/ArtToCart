using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtToCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Basket20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemPhotoUri",
                table: "BasketItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemPhotoUri",
                table: "BasketItem",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

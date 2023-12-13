using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtToCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductReviews21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingValue",
                table: "ItemReview",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingValue",
                table: "ItemReview");
        }
    }
}

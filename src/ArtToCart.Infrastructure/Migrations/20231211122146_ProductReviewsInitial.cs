using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtToCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductReviewsInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_catalog_type_CatalogTypeId",
                table: "CatalogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_product_images_CatalogItems_CatalogItemId",
                table: "product_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_images",
                table: "product_images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_catalog_type",
                table: "catalog_type");

            migrationBuilder.RenameTable(
                name: "product_images",
                newName: "ProductImages");

            migrationBuilder.RenameTable(
                name: "catalog_type",
                newName: "CatalogTypes");

            migrationBuilder.RenameIndex(
                name: "IX_product_images_Id",
                table: "ProductImages",
                newName: "IX_ProductImages_Id");

            migrationBuilder.RenameIndex(
                name: "IX_product_images_CatalogItemId",
                table: "ProductImages",
                newName: "IX_ProductImages_CatalogItemId");

            migrationBuilder.AddColumn<int>(
                name: "AverageRating_NumRatings",
                table: "CatalogItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AverageRating_Value",
                table: "CatalogItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "CatalogItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogTypes",
                table: "CatalogTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ItemReview",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CatalogItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemReview_CatalogItems_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemReview_CatalogItemId",
                table: "ItemReview",
                column: "CatalogItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReview_Id",
                table: "ItemReview",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_CatalogTypes_CatalogTypeId",
                table: "CatalogItems",
                column: "CatalogTypeId",
                principalTable: "CatalogTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_CatalogItems_CatalogItemId",
                table: "ProductImages",
                column: "CatalogItemId",
                principalTable: "CatalogItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_CatalogTypes_CatalogTypeId",
                table: "CatalogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_CatalogItems_CatalogItemId",
                table: "ProductImages");

            migrationBuilder.DropTable(
                name: "ItemReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogTypes",
                table: "CatalogTypes");

            migrationBuilder.DropColumn(
                name: "AverageRating_NumRatings",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "AverageRating_Value",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "CatalogItems");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "product_images");

            migrationBuilder.RenameTable(
                name: "CatalogTypes",
                newName: "catalog_type");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_Id",
                table: "product_images",
                newName: "IX_product_images_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_CatalogItemId",
                table: "product_images",
                newName: "IX_product_images_CatalogItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_images",
                table: "product_images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_catalog_type",
                table: "catalog_type",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_catalog_type_CatalogTypeId",
                table: "CatalogItems",
                column: "CatalogTypeId",
                principalTable: "catalog_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_images_CatalogItems_CatalogItemId",
                table: "product_images",
                column: "CatalogItemId",
                principalTable: "CatalogItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

namespace ArtToCart.Application.Baskets.Shared;

public class BasketItemDto
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string CatalogItemId { get; set; }
    public string BasketId { get; set; }
    public string Basket { get; set; }
}
namespace ArtToCart.Application.Baskets.Shared;

public class BasketItemDto
{
    public string Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string CatalogItemId { get; set; }
    public string BasketId { get; set; }
}

public class UpdateBasketItemDto
{
    public string Id { get; set; }
    public int Quantity { get; set; }
}
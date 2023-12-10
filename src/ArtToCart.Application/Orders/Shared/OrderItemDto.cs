using ArtToCart.Domain.Orders;

namespace ArtToCart.Application.Orders.Shared;

public class OrderItemDto
{
    public CatalogItemOrderedDto ItemOrdered { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
}
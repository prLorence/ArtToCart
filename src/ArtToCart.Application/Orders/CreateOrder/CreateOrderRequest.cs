using ArtToCart.Application.Orders.Shared;

namespace ArtToCart.Application.Orders.CreateOrder;

public class CreateOrderRequest
{
    public string BasketId { get; set; }
    public AddressDto Address { get; set; }
}
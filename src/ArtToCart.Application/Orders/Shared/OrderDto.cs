namespace ArtToCart.Application.Orders.Shared;

public class OrderDto
{
    public decimal Total { get; set; }
    public string BuyerId { get; set; }
    public AddressDto ShippingAddress { get; set; }
    public List<OrderItemDto> Items { get; set; }
}
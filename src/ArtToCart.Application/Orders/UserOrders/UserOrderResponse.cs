using ArtToCart.Application.Orders.Shared;

namespace ArtToCart.Application.Orders.UserOrders;

public record UserOrderResponse(List<OrderDto> Order);
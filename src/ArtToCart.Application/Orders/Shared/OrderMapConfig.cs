using ArtToCart.Domain.Orders;

using Mapster;

namespace ArtToCart.Application.Orders.Shared;

public class OrderMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>()
            .Map(dest => dest.BuyerId, src => src.BuyerId)
            .Map(dest => dest.ShippingAddress, src => src.ShipToAddress)
            .Map(dest => dest.Items, src => src.OrderItems);

        config.NewConfig<OrderItem, OrderItemDto>()
            .Map(dest => dest.ItemOrdered, src => src.ItemOrdered)
            .Map(dest => dest.UnitPrice, src => src.UnitPrice)
            .Map(dest => dest.UnitPrice, src => src.UnitPrice);

        config.NewConfig<CatalogItemOrdered, CatalogItemOrderedDto>()
            .Map(dest => dest.CatalogItemId, src => src.CatalogItemId)
            .Map(dest => dest.PictureUri, src => src.PictureUri)
            .Map(dest => dest.ProductName, src => src.ProductName);

        config.NewConfig<Address, AddressDto>()
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.ZipCode, src => src.ZipCode);
    }
}
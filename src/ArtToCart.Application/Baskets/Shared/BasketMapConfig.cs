using ArtToCart.Domain.Baskets;

using Mapster;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Baskets.Shared;

public class BasketMapConfig : IRegister
{
     public void Register(TypeAdapterConfig config)
     {
         config.NewConfig<Basket, BasketDto>()
             .Map(dest => dest.BasketId, src => src.Id.Value.ToString())
             .Map(dest => dest.BuyerId, src => src.BuyerId)
             .Map(dest => dest.Items, src => src.Items);

         config.NewConfig<BasketItem, BasketItemDto>()
             .Map(dest => dest.Id, src => src.Id.Value.ToString())
             .Map(dest => dest.CatalogItemId, src => src.CatalogItemId)
             .Map(dest => dest.BasketId, src => src.BasketId.Value.ToString());
     }
}
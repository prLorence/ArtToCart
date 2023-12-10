using ArtToCart.Application.Baskets.Shared;

namespace ArtToCart.Application.Baskets.UpdateBasketItems;

public class UpdateBasketItemsRequest
{
    public string BasketId { get; set; }
    public IEnumerable<UpdateBasketItemDto> Items { get; set;}
}
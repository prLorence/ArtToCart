using ArtToCart.Domain.Baskets;

namespace ArtToCart.Application.Baskets.Shared;

public class BasketDto
{
    public string BuyerId { get; set; }
    public List<BasketItemDto> Items { get; set; }
    public int TotalItemCount;
}
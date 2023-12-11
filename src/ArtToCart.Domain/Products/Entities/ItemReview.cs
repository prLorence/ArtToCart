using ArtToCart.Domain.Common;
using ArtToCart.Domain.Products.ValueObjects;

namespace ArtToCart.Domain.Products.Entities;

public class ItemReview : BaseEntity<ItemReviewId>
{
    public string Value { get; }
    public CatalogItemId CatalogItemId { get; }
    public CatalogItem CatalogItem { get; }
    public string BuyerId { get; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now.ToUniversalTime();

#pragma warning disable CS8618
    private ItemReview()
    {

    }
#pragma warning restore CS8618

    public ItemReview(
        ItemReviewId id,
        string value,
        CatalogItemId catalogItemId,
        string buyerId) : base(id)
    {
        CatalogItemId = catalogItemId;
        BuyerId = buyerId;
        Value = value;
    }
}
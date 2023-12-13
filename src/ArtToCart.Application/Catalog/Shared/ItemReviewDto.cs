namespace ArtToCart.Application.Catalog.Shared;

public class ItemReviewDto
{
    public string Value { get; set; }
    public int Rating { get; set; }
    public string BuyerId { get; set; }
    public DateTime CreatedDateTime { get; set; }
}
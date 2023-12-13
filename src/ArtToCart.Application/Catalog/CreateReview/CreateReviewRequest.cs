namespace ArtToCart.Application.Catalog.CreateReview;

public record CreateReviewRequest(
    string CatalogItemId,
    string Value,
    string BuyerId,
    int Rating);

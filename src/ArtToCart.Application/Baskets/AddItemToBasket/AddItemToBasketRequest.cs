namespace ArtToCart.Application.Baskets.AddItemToBasket;

public record AddItemToBasketRequest(string Username, string CatalogItemId, string Size, int Quantity = 1);
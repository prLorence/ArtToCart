namespace ArtToCart.Application.Baskets.AddItemToBasket;

public record AddItemToBasketCommandRequest(string Username, string CatalogItemId, decimal Price, int Quantity = 1);
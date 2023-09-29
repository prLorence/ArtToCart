using ArtToCart.Domain.Common;
using ArtToCart.Domain.ProductRoot.ValueObjects;

namespace ArtToCart.Domain.ProductRoot;

public class Product : BaseEntity<ProductId>
{
    public string Name { get; set;  }
    public double Price { get; set; }
    public string Description { get; set; }
    public string SellerId { get; set; }
    // photo?


    private Product(
        ProductId id,
        string name,
        double price,
        string description,
        string sellerId) : base(id)
    {
        Name = name;
        Price = price;
        Description = description;
        SellerId = sellerId;
    }
#pragma warning disable CS8618
    private Product()
    {
    }
#pragma warning disable CS8618

    public Product Create(string name, double price, string description, string sellerId)
    {
        return new(
            ProductId.CreateUnique(),
            name,
            price,
            description,
            sellerId
        );
    }
}
using ArtToCart.Core.Domain;

namespace ArtToCart.Domain.Products.ValueObjects;

public class ItemReviewId : ValueObject
{    public Guid Value { get; private set; }

     private ItemReviewId(Guid value)
     {
         Value = value;
     }
     protected override IEnumerable<object> GetEqualityComponents()
     {
         yield return Value;
     }

     public static ItemReviewId CreateUnique()
     {
         return new(Guid.NewGuid());
     }

     public static ItemReviewId CreateFrom(string id)
     {
         return new(Guid.Parse(id));
     }

     public static ItemReviewId CreateFrom(Guid id)
     {
         return new(id);
     }

}
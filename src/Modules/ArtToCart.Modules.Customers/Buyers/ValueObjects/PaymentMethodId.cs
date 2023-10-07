using ArtToCart.Core.Domain;

namespace ArtToCart.Modules.Customers.Buyers.ValueObjects;

public class PaymentMethodId : ValueObject
{    public Guid Value { get; private set; }

     private PaymentMethodId(Guid value)
     {
         Value = value;
     }

     public static PaymentMethodId CreateUnique()
     {
         return new PaymentMethodId(Guid.NewGuid());
     }
     protected override IEnumerable<object> GetEqualityComponents()
     {
         yield return Value;
     }

}
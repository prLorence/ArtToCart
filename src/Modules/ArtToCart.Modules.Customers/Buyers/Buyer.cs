using ArtToCart.Core.Domain;
using ArtToCart.Domain.Entities.BuyerAggregate;
using ArtToCart.Modules.Customers.Buyers.ValueObjects;

namespace ArtToCart.Modules.Customers.Buyers;

public class Buyer : BaseEntity<UserName>, IAggregateRoot
{
    private List<PaymentMethod> _paymentMethods = new();
    public IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    private Buyer(UserName userName): base(userName)
    {
    }

    public static Buyer Create(string userName)
    {
        return new Buyer(new UserName(userName));
    }
}
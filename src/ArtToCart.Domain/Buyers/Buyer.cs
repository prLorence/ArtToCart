using ArtToCart.Core.Domain;
using ArtToCart.Domain.Buyers.ValueObjects;
using ArtToCart.Domain.Common;

namespace ArtToCart.Domain.Buyers;

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
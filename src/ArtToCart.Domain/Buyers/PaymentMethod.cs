using ArtToCart.Domain.Buyers.ValueObjects;
using ArtToCart.Domain.Common;

namespace ArtToCart.Domain.Buyers;

public class PaymentMethod : BaseEntity<PaymentMethodId>
{
    public string? Alias { get; private set; }
    public string? CardId { get; private set; } // actual card data must be stored in a PCI compliant system, like Stripe
    public string? Last4 { get; private set; }
}
using ArtToCart.Core.Domain;

namespace ArtToCart.Modules.Customers.Buyers.ValueObjects;

public class UserName : ValueObject
{
    public string Value { get; private set; }

    public UserName(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
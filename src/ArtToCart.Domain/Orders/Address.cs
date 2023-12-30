namespace ArtToCart.Domain.Orders;

public class Address // ValueObject
{
    public string Street { get; private set; }

    public string City { get; private set; }

    public string Province { get; private set; }

    public string Country { get; private set; }

    public string ZipCode { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private Address() { }

    public static Address Create(string street, string city, string province, string country, string zipCode)
    {
        return new Address
        {
            Street = street,
            City = city,
            Province = province,
            Country = country,
            ZipCode = zipCode
        };
    }
}
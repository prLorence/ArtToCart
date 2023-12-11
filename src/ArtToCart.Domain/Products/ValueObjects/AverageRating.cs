using ArtToCart.Core.Domain;

namespace ArtToCart.Domain.Products.ValueObjects;

public class AverageRating : ValueObject
{
    public double Value { get; private set; }
    public int NumRatings { get; private set; }

    private AverageRating(double value, int numRatings)
    {
        Value = value;
        NumRatings = numRatings;
    }

    public static AverageRating CreateNew(double value = 0, int numRatings = 0)
    {
        return new(value, numRatings);
    }

    public void AddNewRating(int rating)
    {
        Value = ((Value * NumRatings) + rating) / ++NumRatings;
    }

    public void RemoveRating(int rating)
    {
        Value = ((Value * NumRatings) - rating) / --NumRatings;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
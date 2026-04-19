using Cratebase.Domain.SharedKernel.Errors;

namespace Cratebase.Domain.Ratings;

public readonly record struct Rating
{
    private Rating(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Rating FromValue(int value)
    {
        return value is < 1 or > 10
            ? throw new DomainException("rating.out_of_range", "Rating must be between 1 and 10")
            : new Rating(value);
    }
}

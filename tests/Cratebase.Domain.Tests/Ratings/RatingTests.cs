using Cratebase.Domain.Ratings;
using Cratebase.Domain.SharedKernel.Errors;

namespace Cratebase.Domain.Tests.Ratings;

public sealed class RatingTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void Rating_accepts_values_from_one_to_ten(int value)
    {
        var rating = Rating.FromValue(value);

        Assert.Equal(value, rating.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    public void Rating_rejects_values_outside_one_to_ten(int value)
    {
        DomainException exception = Assert.Throws<DomainException>(() => Rating.FromValue(value));

        Assert.Equal("rating.out_of_range", exception.Code);
    }
}

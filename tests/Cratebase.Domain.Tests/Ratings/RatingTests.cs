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

    [Fact]
    public void Release_track_rating_summary_rejects_invalid_states()
    {
        DomainException negativeCount = Assert.Throws<DomainException>(() => ReleaseTrackRatingSummary.FromAverage(8m, -1));
        DomainException unratedWithAverage = Assert.Throws<DomainException>(() => ReleaseTrackRatingSummary.FromAverage(8m, 0));

        Assert.Equal("release_track_rating_summary.count_negative", negativeCount.Code);
        Assert.Equal("release_track_rating_summary.invalid_state", unratedWithAverage.Code);
    }
}

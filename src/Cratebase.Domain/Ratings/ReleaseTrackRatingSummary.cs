using Cratebase.Domain.SharedKernel.Errors;

namespace Cratebase.Domain.Ratings;

public sealed class ReleaseTrackRatingSummary
{
    private ReleaseTrackRatingSummary(decimal? averageRating, int ratedTrackCount)
    {
        if (ratedTrackCount < 0)
        {
            throw new DomainException("release_track_rating_summary.count_negative", "Rated track count cannot be negative");
        }

        if (ratedTrackCount == 0 && averageRating is not null)
        {
            throw new DomainException("release_track_rating_summary.invalid_state", "Unrated summary cannot have an average rating");
        }

        if (ratedTrackCount > 0 && averageRating is null)
        {
            throw new DomainException("release_track_rating_summary.invalid_state", "Rated summary must have an average rating");
        }

        AverageRating = averageRating;
        RatedTrackCount = ratedTrackCount;
    }

    public decimal? AverageRating { get; }

    public int RatedTrackCount { get; }

    public static ReleaseTrackRatingSummary Unrated()
    {
        return new ReleaseTrackRatingSummary(null, 0);
    }

    public static ReleaseTrackRatingSummary FromAverage(decimal averageRating, int ratedTrackCount)
    {
        return new ReleaseTrackRatingSummary(averageRating, ratedTrackCount);
    }
}

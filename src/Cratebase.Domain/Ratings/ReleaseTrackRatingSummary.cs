namespace Cratebase.Domain.Ratings;

public sealed class ReleaseTrackRatingSummary
{
    public required decimal? AverageRating { get; init; }

    public required int RatedTrackCount { get; init; }
}

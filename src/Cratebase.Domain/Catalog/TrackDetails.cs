using Cratebase.Domain.Ratings;
using Cratebase.Domain.SharedKernel.Optional;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record TrackDetails
{
    private TrackDetails(OptionalValue<TimeSpan> duration, OptionalValue<Rating> rating)
    {
        Duration = duration;
        Rating = rating;
    }

    public OptionalValue<TimeSpan> Duration { get; }

    public OptionalValue<Rating> Rating { get; }

    public static TrackDetails Empty { get; } = new(Optional.Missing<TimeSpan>(), Optional.Missing<Rating>());

    public TrackDetails WithDuration(TimeSpan duration)
    {
        return new TrackDetails(
            Optional.From(Guard.Positive(duration, nameof(duration), "track.duration_required")),
            Rating);
    }

    public TrackDetails WithRating(Rating rating)
    {
        ArgumentNullException.ThrowIfNull(rating);

        return new TrackDetails(Duration, Optional.From(rating));
    }
}

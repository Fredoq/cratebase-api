using Cratebase.Domain.Ratings;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Track : IEntity<TrackId>, ICreditTarget
{
    public required TrackId Id { get; init; }

    public required string Title { get; init; }

    public string Name => Title;

    public string DisplayName => Title;

    public TimeSpan? Duration { get; init; }

    public Rating? Rating { get; init; }

    public IReadOnlyList<Genre> Genres { get; init; } = [];

    public IReadOnlyList<Tag> Tags { get; init; } = [];

    public static Track Create(TrackId id, string title)
    {
        return new Track
        {
            Id = id,
            Title = Guard.RequiredText(title, nameof(title), "track.title_required")
        };
    }

    public Track WithRating(Rating rating)
    {
        return new Track
        {
            Id = Id,
            Title = Title,
            Duration = Duration,
            Rating = rating,
            Genres = [.. Genres],
            Tags = [.. Tags]
        };
    }
}

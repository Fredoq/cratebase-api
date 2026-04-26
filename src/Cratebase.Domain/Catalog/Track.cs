using Cratebase.Domain.Ratings;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Track : IEntity<TrackId>, ICreditTarget
{
    private Track(
        TrackId id,
        string title,
        TimeSpan? duration,
        Rating? rating,
        IReadOnlyList<Genre> genres,
        IReadOnlyList<Tag> tags)
    {
        Id = id;
        Title = title;
        Duration = duration;
        Rating = rating;
        Genres = genres;
        Tags = tags;
    }

    public TrackId Id { get; }

    public string Title { get; }

    public string Name => Title;

    public string DisplayName => Title;

    public TimeSpan? Duration { get; }

    public Rating? Rating { get; }

    public IReadOnlyList<Genre> Genres { get; }

    public IReadOnlyList<Tag> Tags { get; }

    public static Track Create(TrackId id, string title)
    {
        return new Track(
            id,
            Guard.RequiredText(title, nameof(title), "track.title_required"),
            null,
            null,
            [],
            []);
    }

    public Track WithDuration(TimeSpan duration)
    {
        return Copy(duration: Guard.Positive(duration, nameof(duration), "track.duration_required"));
    }

    public Track WithRating(Rating rating)
    {
        ArgumentNullException.ThrowIfNull(rating);

        return Copy(rating: rating);
    }

    public Track WithGenre(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre);

        return Genres.Contains(genre) ? this : Copy(genres: [.. Genres, genre]);
    }

    public Track WithTag(Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        return Tags.Contains(tag) ? this : Copy(tags: [.. Tags, tag]);
    }

    private Track Copy(
        TimeSpan? duration = null,
        Rating? rating = null,
        IReadOnlyList<Genre>? genres = null,
        IReadOnlyList<Tag>? tags = null)
    {
        return new Track(
            Id,
            Title,
            duration ?? Duration,
            rating ?? Rating,
            genres ?? [.. Genres],
            tags ?? [.. Tags]);
    }
}

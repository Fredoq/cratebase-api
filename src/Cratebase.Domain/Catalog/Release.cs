using Cratebase.Domain.Ratings;
using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Release : IEntity<ReleaseId>, ICreditTarget
{
    private Release(ReleaseState state)
    {
        Id = state.Id;
        Title = state.Title;
        Type = state.Type;
        LabelId = state.LabelId;
        Year = state.Year;
        ReleaseDate = state.ReleaseDate;
        CoverImage = state.CoverImage;
        Rating = state.Rating;
        Tracklist = state.Tracklist;
        Genres = state.Genres;
        Tags = state.Tags;
    }

    public ReleaseId Id { get; }

    public string Title { get; }

    public string Name => Title;

    public string DisplayName => Title;

    public ReleaseType Type { get; }

    public LabelId? LabelId { get; }

    public int? Year { get; }

    public DateOnly? ReleaseDate { get; }

    public CoverImage? CoverImage { get; }

    public Rating? Rating { get; }

    public IReadOnlyList<ReleaseTrack> Tracklist { get; }

    public IReadOnlyList<Genre> Genres { get; }

    public IReadOnlyList<Tag> Tags { get; }

    public static Release Create(ReleaseId id, string title)
    {
        return new Release(new ReleaseState
        {
            Id = id,
            Title = Guard.RequiredText(title, nameof(title), "release.title_required"),
            Type = ReleaseType.Unknown,
            LabelId = null,
            Year = null,
            ReleaseDate = null,
            CoverImage = null,
            Rating = null,
            Tracklist = [],
            Genres = [],
            Tags = []
        });
    }

    public Release WithRating(Rating rating)
    {
        ArgumentNullException.ThrowIfNull(rating);

        return Copy(state => state with
        {
            Rating = rating
        });
    }

    public Release WithTrack(ReleaseTrack releaseTrack)
    {
        ArgumentNullException.ThrowIfNull(releaseTrack);

        EnsureTrackPositionIsUnique(releaseTrack.Position);

        return Copy(state => state with
        {
            Tracklist = [.. Tracklist, releaseTrack]
        });
    }

    public Release WithLabel(LabelId labelId)
    {
        return Copy(state => state with
        {
            LabelId = labelId
        });
    }

    public Release WithType(ReleaseType type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return Copy(state => state with
        {
            Type = type
        });
    }

    public Release WithReleaseYear(int year)
    {
        return Copy(state => state with
        {
            Year = Guard.Positive(year, nameof(year), "release.year_required")
        });
    }

    public Release WithReleaseDate(DateOnly releaseDate)
    {
        return Copy(state => state with
        {
            ReleaseDate = releaseDate
        });
    }

    public Release WithCoverImage(CoverImage coverImage)
    {
        ArgumentNullException.ThrowIfNull(coverImage);

        return Copy(state => state with
        {
            CoverImage = coverImage
        });
    }

    public Release WithGenre(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre);

        return Genres.Contains(genre)
            ? this
            : Copy(state => state with
            {
                Genres = [.. Genres, genre]
            });
    }

    public Release WithTag(Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        return Tags.Contains(tag)
            ? this
            : Copy(state => state with
            {
                Tags = [.. Tags, tag]
            });
    }

    private Release Copy(Func<ReleaseState, ReleaseState> update)
    {
        ArgumentNullException.ThrowIfNull(update);

        return new Release(update(ToState()));
    }

    private void EnsureTrackPositionIsUnique(TrackPosition position)
    {
        if (Tracklist.Any(existing => existing.Position == position))
        {
            throw new DomainException("release_track.position_duplicate", "Release track position already exists");
        }
    }

    private ReleaseState ToState()
    {
        return new ReleaseState
        {
            Id = Id,
            Title = Title,
            Type = Type,
            LabelId = LabelId,
            Year = Year,
            ReleaseDate = ReleaseDate,
            CoverImage = CoverImage,
            Rating = Rating,
            Tracklist = [.. Tracklist],
            Genres = [.. Genres],
            Tags = [.. Tags]
        };
    }

    private sealed record ReleaseState
    {
        public required ReleaseId Id { get; init; }

        public required string Title { get; init; }

        public required ReleaseType Type { get; init; }

        public LabelId? LabelId { get; init; }

        public int? Year { get; init; }

        public DateOnly? ReleaseDate { get; init; }

        public CoverImage? CoverImage { get; init; }

        public Rating? Rating { get; init; }

        public required IReadOnlyList<ReleaseTrack> Tracklist { get; init; }

        public required IReadOnlyList<Genre> Genres { get; init; }

        public required IReadOnlyList<Tag> Tags { get; init; }
    }
}

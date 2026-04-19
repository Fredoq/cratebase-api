using Cratebase.Domain.Ratings;
using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Release : IEntity<ReleaseId>, ICreditTarget
{
    public required ReleaseId Id { get; init; }

    public required string Title { get; init; }

    public string Name => Title;

    public string DisplayName => Title;

    public LabelId? LabelId { get; init; }

    public int? Year { get; init; }

    public DateOnly? ReleaseDate { get; init; }

    public Rating? Rating { get; init; }

    public IReadOnlyList<ReleaseTrack> Tracklist { get; init; } = [];

    public IReadOnlyList<Genre> Genres { get; init; } = [];

    public IReadOnlyList<Tag> Tags { get; init; } = [];

    public static Release Create(ReleaseId id, string title)
    {
        return new Release
        {
            Id = id,
            Title = Guard.RequiredText(title, nameof(title), "release.title_required")
        };
    }

    public Release WithRating(Rating rating)
    {
        Release release = Copy();

        return new Release
        {
            Id = release.Id,
            Title = release.Title,
            LabelId = release.LabelId,
            Year = release.Year,
            ReleaseDate = release.ReleaseDate,
            Rating = rating,
            Tracklist = release.Tracklist,
            Genres = release.Genres,
            Tags = release.Tags
        };
    }

    public Release WithTrack(ReleaseTrack releaseTrack)
    {
        ArgumentNullException.ThrowIfNull(releaseTrack);

        if (releaseTrack.ReleaseId != Id)
        {
            throw new DomainException("release_track.release_mismatch", "Release track must belong to the release");
        }

        Release release = Copy();

        return new Release
        {
            Id = release.Id,
            Title = release.Title,
            LabelId = release.LabelId,
            Year = release.Year,
            ReleaseDate = release.ReleaseDate,
            Rating = release.Rating,
            Tracklist = [.. release.Tracklist, releaseTrack],
            Genres = release.Genres,
            Tags = release.Tags
        };
    }

    public Release WithLabel(LabelId labelId)
    {
        Release release = Copy();

        return new Release
        {
            Id = release.Id,
            Title = release.Title,
            LabelId = labelId,
            Year = release.Year,
            ReleaseDate = release.ReleaseDate,
            Rating = release.Rating,
            Tracklist = release.Tracklist,
            Genres = release.Genres,
            Tags = release.Tags
        };
    }

    private Release Copy()
    {
        return new Release
        {
            Id = Id,
            Title = Title,
            LabelId = LabelId,
            Year = Year,
            ReleaseDate = ReleaseDate,
            Rating = Rating,
            Tracklist = [.. Tracklist],
            Genres = [.. Genres],
            Tags = [.. Tags]
        };
    }
}

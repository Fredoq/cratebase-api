using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Optional;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record ReleaseMetadata
{
    private ReleaseMetadata(
        ReleaseType type,
        OptionalValue<LabelId> labelId,
        OptionalValue<int> year,
        OptionalValue<DateOnly> releaseDate,
        OptionalValue<CoverImage> coverImage)
    {
        Type = type;
        LabelId = labelId;
        Year = year;
        ReleaseDate = releaseDate;
        CoverImage = coverImage;
    }

    public ReleaseType Type { get; }

    public OptionalValue<LabelId> LabelId { get; }

    public OptionalValue<int> Year { get; }

    public OptionalValue<DateOnly> ReleaseDate { get; }

    public OptionalValue<CoverImage> CoverImage { get; }

    public static ReleaseMetadata Empty { get; } = new(
        ReleaseType.Unknown,
        Optional.Missing<LabelId>(),
        Optional.Missing<int>(),
        Optional.Missing<DateOnly>(),
        Optional.Missing<CoverImage>());

    public ReleaseMetadata WithType(ReleaseType type)
    {
        return new ReleaseMetadata(type, LabelId, Year, ReleaseDate, CoverImage);
    }

    public ReleaseMetadata WithLabel(LabelId labelId)
    {
        return new ReleaseMetadata(Type, Optional.From(labelId), Year, ReleaseDate, CoverImage);
    }

    public ReleaseMetadata WithReleaseYear(int year)
    {
        return new ReleaseMetadata(
            Type,
            LabelId,
            Optional.From(Guard.Positive(year, nameof(year), "release.year_required")),
            ReleaseDate,
            CoverImage);
    }

    public ReleaseMetadata WithReleaseDate(DateOnly releaseDate)
    {
        return new ReleaseMetadata(Type, LabelId, Year, Optional.From(releaseDate), CoverImage);
    }

    public ReleaseMetadata WithCoverImage(CoverImage coverImage)
    {
        ArgumentNullException.ThrowIfNull(coverImage);

        return new ReleaseMetadata(Type, LabelId, Year, ReleaseDate, Optional.From(coverImage));
    }
}

using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record TrackPosition
{
    public required int Number { get; init; }

    public string? Disc { get; init; }

    public string? Side { get; init; }

    public static TrackPosition FromNumber(int number)
    {
        return new TrackPosition
        {
            Number = Guard.Positive(number, nameof(number), "track_position.number_required")
        };
    }
}

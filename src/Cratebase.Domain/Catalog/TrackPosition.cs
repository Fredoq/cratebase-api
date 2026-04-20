using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record TrackPosition
{
    private TrackPosition(int number, string? disc, string? side)
    {
        Number = number;
        Disc = disc;
        Side = side;
    }

    public int Number { get; }

    public string? Disc { get; }

    public string? Side { get; }

    public static TrackPosition FromNumber(int number)
    {
        return FromNumber(number, null, null);
    }

    public static TrackPosition FromNumber(int number, string? disc, string? side)
    {
        return new TrackPosition(
            Guard.Positive(number, nameof(number), "track_position.number_required"),
            string.IsNullOrWhiteSpace(disc) ? null : disc.Trim(),
            string.IsNullOrWhiteSpace(side) ? null : side.Trim());
    }
}

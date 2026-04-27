using Cratebase.Domain.SharedKernel.Optional;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record TrackPosition
{
    private TrackPosition(int number, OptionalValue<string> disc, OptionalValue<string> side)
    {
        Number = number;
        Disc = disc;
        Side = side;
    }

    public int Number { get; }

    public OptionalValue<string> Disc { get; }

    public OptionalValue<string> Side { get; }

    public static TrackPosition FromNumber(int number)
    {
        return new TrackPosition(
            Guard.Positive(number, nameof(number), "track_position.number_required"),
            Optional.Missing<string>(),
            Optional.Missing<string>());
    }

    public static TrackPosition FromNumber(int number, string disc, string side)
    {
        return new TrackPosition(
            Guard.Positive(number, nameof(number), "track_position.number_required"),
            string.IsNullOrWhiteSpace(disc) ? Optional.Missing<string>() : Optional.From(disc.Trim()),
            string.IsNullOrWhiteSpace(side) ? Optional.Missing<string>() : Optional.From(side.Trim()));
    }
}

using Cratebase.Domain.SharedKernel.Optional;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record TrackPosition
{
    private TrackPosition(int number, IOptionalValue<string> disc, IOptionalValue<string> side)
    {
        Number = number;
        Disc = disc;
        Side = side;
    }

    public int Number { get; }

    public IOptionalValue<string> Disc { get; }

    public IOptionalValue<string> Side { get; }

    public static TrackPosition FromNumber(int number)
    {
        return new TrackPosition(
            Guard.Positive(number, nameof(number), "track_position.number_required"),
            Optional.Missing<string>(),
            Optional.Missing<string>());
    }

    public static TrackPosition FromNumber(int number, string disc, string side)
    {
        ArgumentNullException.ThrowIfNull(disc);
        ArgumentNullException.ThrowIfNull(side);

        string trimmedDisc = disc.Trim();
        string trimmedSide = side.Trim();

        return new TrackPosition(
            Guard.Positive(number, nameof(number), "track_position.number_required"),
            trimmedDisc.Length == 0 ? Optional.Missing<string>() : Optional.From(trimmedDisc),
            trimmedSide.Length == 0 ? Optional.Missing<string>() : Optional.From(trimmedSide));
    }
}

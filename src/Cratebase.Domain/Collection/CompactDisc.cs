using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record CompactDisc : Medium
{
    public required int DiscCount { get; init; }

    public override string Description => DiscCount == 1 ? "CD" : $"{DiscCount} CDs";

    public static CompactDisc Create(int discCount)
    {
        return new CompactDisc
        {
            DiscCount = Guard.Positive(discCount, nameof(discCount), "compact_disc.disc_count_required")
        };
    }
}

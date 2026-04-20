using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record CompactDisc : IMedium
{
    private CompactDisc(int discCount)
    {
        DiscCount = discCount;
    }

    public int DiscCount { get; }

    public string Description => DiscCount == 1 ? "CD" : $"{DiscCount} CDs";

    public static CompactDisc Create(int discCount)
    {
        return new CompactDisc(Guard.Positive(discCount, nameof(discCount), "compact_disc.disc_count_required"));
    }
}

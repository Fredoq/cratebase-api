using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record VinylRecord : Medium
{
    public required string FormatDescription { get; init; }

    public override string Description => FormatDescription;

    public static VinylRecord Create(string formatDescription)
    {
        return new VinylRecord
        {
            FormatDescription = Guard.RequiredText(formatDescription, nameof(formatDescription), "vinyl_record.format_required")
        };
    }
}

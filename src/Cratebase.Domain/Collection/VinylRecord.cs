using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record VinylRecord : IMedium
{
    private VinylRecord(string formatDescription)
    {
        FormatDescription = formatDescription;
    }

    public string FormatDescription { get; }

    public string Description => FormatDescription;

    public static VinylRecord Create(string formatDescription)
    {
        return new VinylRecord(Guard.RequiredText(formatDescription, nameof(formatDescription), "vinyl_record.format_required"));
    }
}

namespace Cratebase.Domain.Collection;

public sealed record DigitalFile : Medium
{
    public required FilePath Path { get; init; }

    public required AudioFileFormat Format { get; init; }

    public override string Description => $"{Format.Code} file";

    public static DigitalFile Create(FilePath path, AudioFileFormat format)
    {
        ArgumentNullException.ThrowIfNull(format);

        return new DigitalFile
        {
            Path = path,
            Format = format
        };
    }
}

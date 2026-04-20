namespace Cratebase.Domain.Collection;

public sealed record DigitalFile : Medium
{
    private DigitalFile(FilePath path, AudioFileFormat format)
    {
        Path = path;
        Format = format;
    }

    public FilePath Path { get; }

    public AudioFileFormat Format { get; }

    public override string Description => $"{Format.Code} file";

    public static DigitalFile Create(FilePath path, AudioFileFormat format)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(format);

        return new DigitalFile(path, format);
    }
}

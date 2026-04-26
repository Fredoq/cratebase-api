using Cratebase.Domain.SharedKernel.Errors;

namespace Cratebase.Domain.Collection;

public sealed record DigitalFile : IMedium
{
    private DigitalFile(FilePath path, AudioFileFormat format, FileImportIdentity? importIdentity)
    {
        Path = path;
        Format = format;
        ImportIdentity = importIdentity;
    }

    public FilePath Path { get; }

    public AudioFileFormat Format { get; }

    public FileImportIdentity? ImportIdentity { get; }

    public string Description => $"{Format.Code} file";

    public static DigitalFile Create(FilePath path, AudioFileFormat format)
    {
        return Create(path, format, null);
    }

    public static DigitalFile Create(FilePath path, AudioFileFormat format, FileImportIdentity? importIdentity)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(format);

        return importIdentity is not null && importIdentity.Path != path
            ? throw new DomainException("digital_file.import_identity_path_mismatch", "Digital file import identity path must match the file path")
            : new DigitalFile(path, format, importIdentity);
    }
}

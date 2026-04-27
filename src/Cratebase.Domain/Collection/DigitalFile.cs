using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Optional;

namespace Cratebase.Domain.Collection;

public sealed record DigitalFile : IMedium
{
    private DigitalFile(FilePath path, AudioFileFormat format, OptionalValue<FileImportIdentity> importIdentity)
    {
        Path = path;
        Format = format;
        ImportIdentity = importIdentity;
    }

    public FilePath Path { get; }

    public AudioFileFormat Format { get; }

    public OptionalValue<FileImportIdentity> ImportIdentity { get; }

    public string Description => "digital file";

    public static DigitalFile Create(FilePath path, AudioFileFormat format)
    {
        ArgumentNullException.ThrowIfNull(path);

        return new DigitalFile(path, format, Optional.Missing<FileImportIdentity>());
    }

    public static DigitalFile Create(FilePath path, AudioFileFormat format, FileImportIdentity importIdentity)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(importIdentity);

        return importIdentity.Path != path
            ? throw new DomainException("digital_file.import_identity_path_mismatch", "Digital file import identity path must match the file path")
            : new DigitalFile(path, format, Optional.From(importIdentity));
    }
}

using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record FileImportIdentity
{
    private FileImportIdentity(FilePath path, long sizeBytes, DateTimeOffset lastModifiedAt, string? contentHash)
    {
        Path = path;
        SizeBytes = sizeBytes;
        LastModifiedAt = lastModifiedAt;
        ContentHash = contentHash;
    }

    public FilePath Path { get; }

    public long SizeBytes { get; }

    public DateTimeOffset LastModifiedAt { get; }

    public string? ContentHash { get; }

    public static FileImportIdentity Create(
        FilePath path,
        long sizeBytes,
        DateTimeOffset lastModifiedAt,
        string? contentHash = null)
    {
        ArgumentNullException.ThrowIfNull(path);

        return new FileImportIdentity(
            path,
            Guard.Positive(sizeBytes, nameof(sizeBytes), "file_import_identity.size_required"),
            lastModifiedAt,
            string.IsNullOrWhiteSpace(contentHash) ? null : contentHash.Trim().ToLowerInvariant());
    }
}

using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record FilePath
{
    private FilePath(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static FilePath FromAbsolutePath(string path)
    {
        string value = Guard.RequiredText(path, nameof(path), "file_path.path_required");

        return !Path.IsPathFullyQualified(value)
            ? throw new DomainException("file_path.not_absolute", "File path must be absolute")
            : new FilePath(value);
    }

    public override string ToString()
    {
        return Value;
    }
}

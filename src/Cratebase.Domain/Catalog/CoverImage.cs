using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record CoverImage
{
    private CoverImage(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public static CoverImage FromPath(string path)
    {
        return new CoverImage(Guard.RequiredText(path, nameof(path), "cover_image.path_required"));
    }
}

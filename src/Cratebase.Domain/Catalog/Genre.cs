using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record Genre
{
    public required string Name { get; init; }

    public static Genre FromName(string name)
    {
        return new Genre
        {
            Name = Guard.RequiredText(name, nameof(name), "genre.name_required")
        };
    }
}

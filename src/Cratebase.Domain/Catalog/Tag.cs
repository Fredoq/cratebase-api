using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record Tag
{
    public required string Name { get; init; }

    public static Tag FromName(string name)
    {
        return new Tag
        {
            Name = Guard.RequiredText(name, nameof(name), "tag.name_required")
        };
    }
}

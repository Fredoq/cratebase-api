using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record OtherMedium : Medium
{
    public required string Name { get; init; }

    public override string Description => Name;

    public static OtherMedium Create(string name)
    {
        return new OtherMedium
        {
            Name = Guard.RequiredText(name, nameof(name), "other_medium.name_required")
        };
    }
}

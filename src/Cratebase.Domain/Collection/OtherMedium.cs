using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record OtherMedium : IMedium
{
    private OtherMedium(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public string Description => Name;

    public static OtherMedium Create(string name)
    {
        return new OtherMedium(Guard.RequiredText(name, nameof(name), "other_medium.name_required"));
    }
}

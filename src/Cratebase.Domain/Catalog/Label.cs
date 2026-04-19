using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Label : IEntity<LabelId>, INamedEntity
{
    public required LabelId Id { get; init; }

    public required string Name { get; init; }

    public static Label Create(LabelId id, string name)
    {
        return new Label
        {
            Id = id,
            Name = Guard.RequiredText(name, nameof(name), "label.name_required")
        };
    }
}

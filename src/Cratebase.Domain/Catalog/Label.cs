using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Label : IEntity<LabelId>, INamedEntity
{
    private Label(LabelId id, string name)
    {
        Id = id;
        Name = name;
    }

    public LabelId Id { get; }

    public string Name { get; }

    public static Label Create(LabelId id, string name)
    {
        return new Label(id, Guard.RequiredText(name, nameof(name), "label.name_required"));
    }
}

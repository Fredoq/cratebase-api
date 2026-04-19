using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;

namespace Cratebase.Domain.Collection;

public sealed class OwnedItem : IEntity<OwnedItemId>
{
    public required OwnedItemId Id { get; init; }

    public required OwnedItemTarget Target { get; init; }

    public required OwnershipStatus Status { get; init; }

    public required Medium Medium { get; init; }

    public string? Condition { get; init; }

    public string? StorageLocation { get; init; }

    public static OwnedItem Create(OwnedItemId id, OwnedItemTarget target, OwnershipStatus status, Medium medium)
    {
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(status);
        ArgumentNullException.ThrowIfNull(medium);

        return new OwnedItem
        {
            Id = id,
            Target = target,
            Status = status,
            Medium = medium
        };
    }
}

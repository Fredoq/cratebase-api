using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;

namespace Cratebase.Domain.Collection;

public sealed class OwnedItem : IEntity<OwnedItemId>
{
    private OwnedItem(
        OwnedItemId id,
        OwnedItemTarget target,
        OwnedItemHolding holding)
    {
        Id = id;
        Target = target;
        Holding = holding;
    }

    public OwnedItemId Id { get; }

    public OwnedItemTarget Target { get; }

    public OwnedItemHolding Holding { get; }

    public static OwnedItem Create(OwnedItemId id, OwnedItemTarget target, OwnershipStatus status, IMedium medium)
    {
        ArgumentNullException.ThrowIfNull(target);

        return new OwnedItem(id, target, OwnedItemHolding.Create(status, medium));
    }

    public OwnedItem WithStatus(OwnershipStatus status)
    {
        return new OwnedItem(Id, Target, Holding.WithStatus(status));
    }

    public OwnedItem WithCondition(ItemCondition condition)
    {
        return new OwnedItem(Id, Target, Holding.WithDetails(Holding.Details.WithCondition(condition)));
    }

    public OwnedItem WithStorageLocation(StorageLocation storageLocation)
    {
        return new OwnedItem(Id, Target, Holding.WithDetails(Holding.Details.WithStorageLocation(storageLocation)));
    }
}

using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;

namespace Cratebase.Domain.Collection;

public sealed class OwnedItem : IEntity<OwnedItemId>
{
    private OwnedItem(
        OwnedItemId id,
        OwnedItemTarget target,
        OwnershipStatus status,
        IMedium medium,
        ItemCondition? condition,
        StorageLocation? storageLocation)
    {
        Id = id;
        Target = target;
        Status = status;
        Medium = medium;
        Condition = condition;
        StorageLocation = storageLocation;
    }

    public OwnedItemId Id { get; }

    public OwnedItemTarget Target { get; }

    public OwnershipStatus Status { get; }

    public IMedium Medium { get; }

    public ItemCondition? Condition { get; }

    public StorageLocation? StorageLocation { get; }

    public static OwnedItem Create(OwnedItemId id, OwnedItemTarget target, OwnershipStatus status, IMedium medium)
    {
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(status);
        ArgumentNullException.ThrowIfNull(medium);

        return new OwnedItem(id, target, status, medium, null, null);
    }

    public OwnedItem WithStatus(OwnershipStatus status)
    {
        ArgumentNullException.ThrowIfNull(status);

        return Copy(status: status);
    }

    public OwnedItem WithCondition(ItemCondition condition)
    {
        ArgumentNullException.ThrowIfNull(condition);

        return Copy(condition: condition);
    }

    public OwnedItem WithStorageLocation(StorageLocation storageLocation)
    {
        ArgumentNullException.ThrowIfNull(storageLocation);

        return Copy(storageLocation: storageLocation);
    }

    private OwnedItem Copy(
        OwnershipStatus? status = null,
        ItemCondition? condition = null,
        StorageLocation? storageLocation = null)
    {
        return new OwnedItem(
            Id,
            Target,
            status ?? Status,
            Medium,
            condition ?? Condition,
            storageLocation ?? StorageLocation);
    }
}

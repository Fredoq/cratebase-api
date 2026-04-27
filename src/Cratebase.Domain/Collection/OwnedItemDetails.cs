using Cratebase.Domain.SharedKernel.Optional;

namespace Cratebase.Domain.Collection;

public sealed record OwnedItemDetails
{
    private OwnedItemDetails(OptionalValue<ItemCondition> condition, OptionalValue<StorageLocation> storageLocation)
    {
        Condition = condition;
        StorageLocation = storageLocation;
    }

    public OptionalValue<ItemCondition> Condition { get; }

    public OptionalValue<StorageLocation> StorageLocation { get; }

    public static OwnedItemDetails Empty { get; } = new(
        Optional.Missing<ItemCondition>(),
        Optional.Missing<StorageLocation>());

    public OwnedItemDetails WithCondition(ItemCondition condition)
    {
        return new OwnedItemDetails(Optional.From(condition), StorageLocation);
    }

    public OwnedItemDetails WithStorageLocation(StorageLocation storageLocation)
    {
        ArgumentNullException.ThrowIfNull(storageLocation);

        return new OwnedItemDetails(Condition, Optional.From(storageLocation));
    }
}

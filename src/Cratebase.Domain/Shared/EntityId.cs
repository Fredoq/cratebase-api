namespace Cratebase.Domain.Shared;

public readonly record struct EntityId
{
    public EntityId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Entity id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public Guid Value { get; }

    public static EntityId New()
    {
        return new EntityId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString("D");
    }
}

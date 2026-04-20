namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct TrackRelationId
{
    public TrackRelationId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static TrackRelationId New()
    {
        return new TrackRelationId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

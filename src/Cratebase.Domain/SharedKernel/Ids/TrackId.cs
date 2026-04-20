namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct TrackId
{
    public TrackId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static TrackId New()
    {
        return new TrackId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct ArtistId
{
    public ArtistId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ArtistId New()
    {
        return new ArtistId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

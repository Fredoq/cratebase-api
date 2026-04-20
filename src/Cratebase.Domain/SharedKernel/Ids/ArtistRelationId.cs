namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct ArtistRelationId
{
    public ArtistRelationId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ArtistRelationId New()
    {
        return new ArtistRelationId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

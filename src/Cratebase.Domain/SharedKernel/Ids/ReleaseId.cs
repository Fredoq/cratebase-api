namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct ReleaseId
{
    public ReleaseId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ReleaseId New()
    {
        return new ReleaseId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

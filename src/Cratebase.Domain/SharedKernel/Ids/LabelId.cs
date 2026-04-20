namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct LabelId
{
    public LabelId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static LabelId New()
    {
        return new LabelId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

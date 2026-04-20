namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct CreditId
{
    public CreditId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static CreditId New()
    {
        return new CreditId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

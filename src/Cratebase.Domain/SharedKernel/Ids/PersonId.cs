namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct PersonId
{
    public PersonId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PersonId New()
    {
        return new PersonId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

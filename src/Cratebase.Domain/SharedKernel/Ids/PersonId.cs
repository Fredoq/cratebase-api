namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct PersonId(Guid Value)
{
    public static PersonId New()
    {
        return new PersonId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

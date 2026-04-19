namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct GroupId(Guid Value)
{
    public static GroupId New()
    {
        return new GroupId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

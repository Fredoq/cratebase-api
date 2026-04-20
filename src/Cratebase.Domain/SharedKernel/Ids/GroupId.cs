namespace Cratebase.Domain.SharedKernel.Ids;

public readonly record struct GroupId
{
    public GroupId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static GroupId New()
    {
        return new GroupId(Guid.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

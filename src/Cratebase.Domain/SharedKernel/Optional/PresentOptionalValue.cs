namespace Cratebase.Domain.SharedKernel.Optional;

public sealed record PresentOptionalValue<T> : OptionalValue<T>
    where T : notnull
{
    public PresentOptionalValue(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Value = value;
    }

    public override bool HasValue => true;

    public T Value { get; }

    public override TResult Match<TResult>(Func<T, TResult> whenPresent, Func<TResult> whenMissing)
    {
        ArgumentNullException.ThrowIfNull(whenPresent);
        ArgumentNullException.ThrowIfNull(whenMissing);

        return whenPresent(Value);
    }
}

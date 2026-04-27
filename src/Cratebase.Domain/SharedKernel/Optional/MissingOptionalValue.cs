namespace Cratebase.Domain.SharedKernel.Optional;

public sealed record MissingOptionalValue<T> : OptionalValue<T>
    where T : notnull
{
    public override bool HasValue => false;

    public override TResult Match<TResult>(Func<T, TResult> whenPresent, Func<TResult> whenMissing)
    {
        ArgumentNullException.ThrowIfNull(whenPresent);
        ArgumentNullException.ThrowIfNull(whenMissing);

        return whenMissing();
    }
}

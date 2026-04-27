namespace Cratebase.Domain.SharedKernel.Optional;

public abstract record OptionalValue<T>
    where T : notnull
{
    public abstract bool HasValue { get; }

    public abstract TResult Match<TResult>(Func<T, TResult> whenPresent, Func<TResult> whenMissing)
        where TResult : notnull;
}

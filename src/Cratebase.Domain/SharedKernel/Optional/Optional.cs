namespace Cratebase.Domain.SharedKernel.Optional;

public static class Optional
{
    public static OptionalValue<T> Missing<T>()
        where T : notnull
    {
        return new MissingOptionalValue<T>();
    }

    public static OptionalValue<T> From<T>(T value)
        where T : notnull
    {
        return new PresentOptionalValue<T>(value);
    }
}

using Cratebase.Domain.SharedKernel.Errors;

namespace Cratebase.Domain.SharedKernel.Validation;

internal static class Guard
{
    public static string RequiredText(string? value, string fieldName, string code)
    {
        return string.IsNullOrWhiteSpace(value)
            ? throw new DomainException(code, $"{fieldName} is required")
            : value.Trim();
    }

    public static int Positive(int value, string fieldName, string code)
    {
        return value <= 0
            ? throw new DomainException(code, $"{fieldName} must be positive")
            : value;
    }
}

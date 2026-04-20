using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record OwnershipStatus
{
    private OwnershipStatus(string code)
    {
        Code = code;
    }

    public string Code { get; }

    public static OwnershipStatus Owned { get; } = FromCode("owned");

    public static OwnershipStatus Wanted { get; } = FromCode("wanted");

    public static OwnershipStatus Sold { get; } = FromCode("sold");

    public static OwnershipStatus NeedsDigitization { get; } = FromCode("needs_digitization");

    public static OwnershipStatus FromCode(string code)
    {
        return new OwnershipStatus(Guard.RequiredText(code, nameof(code), "ownership_status.code_required").ToLowerInvariant());
    }
}

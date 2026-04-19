using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record OwnershipStatus
{
    public required string Code { get; init; }

    public static OwnershipStatus Owned => FromCode("owned");

    public static OwnershipStatus Wanted => FromCode("wanted");

    public static OwnershipStatus Sold => FromCode("sold");

    public static OwnershipStatus NeedsDigitization => FromCode("needs_digitization");

    public static OwnershipStatus FromCode(string code)
    {
        return new OwnershipStatus
        {
            Code = Guard.RequiredText(code, nameof(code), "ownership_status.code_required").ToLowerInvariant()
        };
    }
}

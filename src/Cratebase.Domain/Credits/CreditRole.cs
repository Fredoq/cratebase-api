using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Credits;

public sealed record CreditRole
{
    public required string Code { get; init; }

    public static CreditRole MainArtist => FromCode("main_artist");

    public static CreditRole FeaturedArtist => FromCode("featured_artist");

    public static CreditRole Remixer => FromCode("remixer");

    public static CreditRole Producer => FromCode("producer");

    public static CreditRole FromCode(string code)
    {
        return new CreditRole
        {
            Code = Guard.RequiredText(code, nameof(code), "credit_role.code_required").ToLowerInvariant()
        };
    }
}

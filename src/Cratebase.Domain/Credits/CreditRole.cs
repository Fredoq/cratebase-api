using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Credits;

public sealed record CreditRole
{
    private CreditRole(string code)
    {
        Code = code;
    }

    public string Code { get; }

    public static CreditRole MainArtist { get; } = FromCode("main_artist");

    public static CreditRole FeaturedArtist { get; } = FromCode("featured_artist");

    public static CreditRole Remixer { get; } = FromCode("remixer");

    public static CreditRole Producer { get; } = FromCode("producer");

    public static CreditRole FromCode(string code)
    {
        return new CreditRole(Guard.RequiredText(code, nameof(code), "credit_role.code_required").ToLowerInvariant());
    }
}

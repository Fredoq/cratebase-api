using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed record ReleaseType
{
    private ReleaseType(string code)
    {
        Code = code;
    }

    public string Code { get; }

    public static ReleaseType Unknown { get; } = FromCode("unknown");

    public static ReleaseType Album { get; } = FromCode("album");

    public static ReleaseType Ep { get; } = FromCode("ep");

    public static ReleaseType SingleRelease { get; } = FromCode("single");

    public static ReleaseType Compilation { get; } = FromCode("compilation");

    public static ReleaseType Bootleg { get; } = FromCode("bootleg");

    public static ReleaseType Mixtape { get; } = FromCode("mixtape");

    public static ReleaseType Promo { get; } = FromCode("promo");

    public static ReleaseType Other { get; } = FromCode("other");

    public static ReleaseType FromCode(string code)
    {
        return new ReleaseType(Guard.RequiredText(code, nameof(code), "release_type.code_required").ToLowerInvariant());
    }
}

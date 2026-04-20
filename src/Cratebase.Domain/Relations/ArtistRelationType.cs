using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Relations;

public sealed record ArtistRelationType
{
    private ArtistRelationType(string code)
    {
        Code = code;
    }

    public string Code { get; }

    public static ArtistRelationType Alias { get; } = FromCode("alias");

    public static ArtistRelationType MemberOf { get; } = FromCode("member_of");

    public static ArtistRelationType SoloProject { get; } = FromCode("solo_project");

    public static ArtistRelationType Collaboration { get; } = FromCode("collaboration");

    public static ArtistRelationType FromCode(string code)
    {
        return new ArtistRelationType(Guard.RequiredText(code, nameof(code), "artist_relation_type.code_required").ToLowerInvariant());
    }
}

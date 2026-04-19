using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Relations;

public sealed record ArtistRelationType
{
    public required string Code { get; init; }

    public static ArtistRelationType Alias => FromCode("alias");

    public static ArtistRelationType MemberOf => FromCode("member_of");

    public static ArtistRelationType SoloProject => FromCode("solo_project");

    public static ArtistRelationType Collaboration => FromCode("collaboration");

    public static ArtistRelationType FromCode(string code)
    {
        return new ArtistRelationType
        {
            Code = Guard.RequiredText(code, nameof(code), "artist_relation_type.code_required").ToLowerInvariant()
        };
    }
}

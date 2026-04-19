using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Relations;

public sealed record TrackRelationType
{
    public required string Code { get; init; }

    public static TrackRelationType RemixOf => FromCode("remix_of");

    public static TrackRelationType VersionOf => FromCode("version_of");

    public static TrackRelationType EditOf => FromCode("edit_of");

    public static TrackRelationType FromCode(string code)
    {
        return new TrackRelationType
        {
            Code = Guard.RequiredText(code, nameof(code), "track_relation_type.code_required").ToLowerInvariant()
        };
    }
}

using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Relations;

public sealed record TrackRelationType
{
    private TrackRelationType(string code)
    {
        Code = code;
    }

    public string Code { get; }

    public static TrackRelationType RemixOf { get; } = FromCode("remix_of");

    public static TrackRelationType VersionOf { get; } = FromCode("version_of");

    public static TrackRelationType EditOf { get; } = FromCode("edit_of");

    public static TrackRelationType FromCode(string code)
    {
        return new TrackRelationType(Guard.RequiredText(code, nameof(code), "track_relation_type.code_required").ToLowerInvariant());
    }
}

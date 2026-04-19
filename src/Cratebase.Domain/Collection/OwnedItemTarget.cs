using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Collection;

public sealed record OwnedItemTarget
{
    private OwnedItemTarget()
    {
    }

    public ReleaseId? ReleaseId { get; init; }

    public TrackId? TrackId { get; init; }

    public bool IsRelease => ReleaseId is not null;

    public bool IsTrack => TrackId is not null;

    public static OwnedItemTarget ForRelease(ReleaseId releaseId)
    {
        return Create(releaseId, null);
    }

    public static OwnedItemTarget ForTrack(TrackId trackId)
    {
        return Create(null, trackId);
    }

    public static OwnedItemTarget Create(ReleaseId? releaseId, TrackId? trackId)
    {
        return (releaseId, trackId) switch
        {
            ({ } release, null) => new OwnedItemTarget
            {
                ReleaseId = release
            },
            (null, { } track) => new OwnedItemTarget
            {
                TrackId = track
            },
            ({ }, { }) => throw new DomainException("owned_item_target.ambiguous", "Owned item target cannot reference both release and track"),
            _ => throw new DomainException("owned_item_target.empty", "Owned item target must reference a release or track")
        };
    }
}

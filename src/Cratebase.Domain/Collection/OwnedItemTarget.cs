using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Collection;

public abstract record OwnedItemTarget
{
    public abstract bool IsRelease { get; }

    public abstract bool IsTrack { get; }

    public static OwnedItemTarget ForRelease(ReleaseId releaseId)
    {
        return ReleaseOwnedItemTarget.Create(releaseId);
    }

    public static OwnedItemTarget ForTrack(TrackId trackId)
    {
        return TrackOwnedItemTarget.Create(trackId);
    }
}

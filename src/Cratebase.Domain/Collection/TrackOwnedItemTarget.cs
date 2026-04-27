using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Collection;

public sealed class TrackOwnedItemTarget : OwnedItemTarget
{
    private TrackOwnedItemTarget(TrackId trackId)
    {
        TrackId = trackId;
    }

    public override bool IsRelease => false;

    public override bool IsTrack => true;

    public TrackId TrackId { get; }

    public static TrackOwnedItemTarget Create(TrackId trackId)
    {
        return new TrackOwnedItemTarget(trackId);
    }
}

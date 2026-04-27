using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Credits;

public sealed record TrackCreditTarget : CreditTarget
{
    private TrackCreditTarget(TrackId trackId)
    {
        TrackId = trackId;
    }

    public override bool IsRelease => false;

    public override bool IsTrack => true;

    public TrackId TrackId { get; }

    public static TrackCreditTarget Create(TrackId trackId)
    {
        return new TrackCreditTarget(trackId);
    }
}

using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Credits;

public sealed record CreditTarget
{
    private CreditTarget()
    {
    }

    public ReleaseId? ReleaseId { get; private init; }

    public TrackId? TrackId { get; private init; }

    public bool IsRelease => ReleaseId is not null;

    public bool IsTrack => TrackId is not null;

    public static CreditTarget ForRelease(ReleaseId releaseId)
    {
        return Create(releaseId, null);
    }

    public static CreditTarget ForTrack(TrackId trackId)
    {
        return Create(null, trackId);
    }

    public static CreditTarget Create(ReleaseId? releaseId, TrackId? trackId)
    {
        return (releaseId, trackId) switch
        {
            ({ } release, null) => new CreditTarget
            {
                ReleaseId = release
            },
            (null, { } track) => new CreditTarget
            {
                TrackId = track
            },
            ({ }, { }) => throw new DomainException("credit_target.ambiguous", "Credit target cannot reference both release and track"),
            (null, null) => throw new DomainException("credit_target.empty", "Credit target must reference a release or track")
        };
    }
}

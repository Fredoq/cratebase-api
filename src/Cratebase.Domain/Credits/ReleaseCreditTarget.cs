using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Credits;

public sealed record ReleaseCreditTarget : CreditTarget
{
    private ReleaseCreditTarget(ReleaseId releaseId)
    {
        ReleaseId = releaseId;
    }

    public override bool IsRelease => true;

    public override bool IsTrack => false;

    public ReleaseId ReleaseId { get; }

    public static ReleaseCreditTarget Create(ReleaseId releaseId)
    {
        return new ReleaseCreditTarget(releaseId);
    }
}

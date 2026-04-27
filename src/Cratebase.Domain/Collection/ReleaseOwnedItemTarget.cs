using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Collection;

public sealed class ReleaseOwnedItemTarget : OwnedItemTarget
{
    private ReleaseOwnedItemTarget(ReleaseId releaseId)
    {
        ReleaseId = releaseId;
    }

    public override bool IsRelease => true;

    public override bool IsTrack => false;

    public ReleaseId ReleaseId { get; }

    public static ReleaseOwnedItemTarget Create(ReleaseId releaseId)
    {
        return new ReleaseOwnedItemTarget(releaseId);
    }
}

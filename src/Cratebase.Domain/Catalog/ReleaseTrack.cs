using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Catalog;

public sealed class ReleaseTrack
{
    public required ReleaseId ReleaseId { get; init; }

    public required TrackId TrackId { get; init; }

    public required TrackPosition Position { get; init; }

    public string? TitleOverride { get; init; }

    public static ReleaseTrack Create(ReleaseId releaseId, TrackId trackId, TrackPosition position)
    {
        return new ReleaseTrack
        {
            ReleaseId = releaseId,
            TrackId = trackId,
            Position = position
        };
    }
}

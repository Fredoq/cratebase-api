using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Catalog;

public sealed class ReleaseTrack
{
    private ReleaseTrack(ReleaseId releaseId, TrackId trackId, TrackPosition position, string? titleOverride)
    {
        ReleaseId = releaseId;
        TrackId = trackId;
        Position = position;
        TitleOverride = titleOverride;
    }

    public ReleaseId ReleaseId { get; }

    public TrackId TrackId { get; }

    public TrackPosition Position { get; }

    public string? TitleOverride { get; }

    public static ReleaseTrack Create(ReleaseId releaseId, TrackId trackId, TrackPosition position)
    {
        return Create(releaseId, trackId, position, null);
    }

    public static ReleaseTrack Create(ReleaseId releaseId, TrackId trackId, TrackPosition position, string? titleOverride)
    {
        ArgumentNullException.ThrowIfNull(position);

        return new ReleaseTrack(
            releaseId,
            trackId,
            position,
            string.IsNullOrWhiteSpace(titleOverride) ? null : titleOverride.Trim());
    }
}

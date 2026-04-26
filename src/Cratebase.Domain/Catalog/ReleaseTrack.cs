using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Catalog;

public sealed class ReleaseTrack
{
    private ReleaseTrack(TrackId trackId, TrackPosition position, string? titleOverride)
    {
        TrackId = trackId;
        Position = position;
        TitleOverride = titleOverride;
    }

    public TrackId TrackId { get; }

    public TrackPosition Position { get; }

    public string? TitleOverride { get; }

    public static ReleaseTrack Create(TrackId trackId, TrackPosition position)
    {
        return Create(trackId, position, null);
    }

    public static ReleaseTrack Create(TrackId trackId, TrackPosition position, string? titleOverride)
    {
        ArgumentNullException.ThrowIfNull(position);

        return new ReleaseTrack(
            trackId,
            position,
            string.IsNullOrWhiteSpace(titleOverride) ? null : titleOverride.Trim());
    }
}

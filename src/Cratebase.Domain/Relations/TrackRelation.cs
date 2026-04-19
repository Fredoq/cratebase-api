using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;

namespace Cratebase.Domain.Relations;

public sealed class TrackRelation : IEntity<TrackRelationId>
{
    public required TrackRelationId Id { get; init; }

    public required TrackId SourceTrackId { get; init; }

    public required TrackId TargetTrackId { get; init; }

    public required TrackRelationType Type { get; init; }

    public static TrackRelation Create(
        TrackRelationId id,
        TrackId sourceTrackId,
        TrackId targetTrackId,
        TrackRelationType type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return sourceTrackId == targetTrackId
            ? throw new DomainException("track_relation.self_relation", "Track relation cannot reference the same track twice")
            : new TrackRelation
            {
                Id = id,
                SourceTrackId = sourceTrackId,
                TargetTrackId = targetTrackId,
                Type = type
            };
    }
}

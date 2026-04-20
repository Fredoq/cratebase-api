using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;

namespace Cratebase.Domain.Relations;

public sealed class TrackRelation : IEntity<TrackRelationId>
{
    private TrackRelation(
        TrackRelationId id,
        TrackId sourceTrackId,
        TrackId targetTrackId,
        TrackRelationType relationType)
    {
        Id = id;
        SourceTrackId = sourceTrackId;
        TargetTrackId = targetTrackId;
        RelationType = relationType;
    }

    public TrackRelationId Id { get; }

    public TrackId SourceTrackId { get; }

    public TrackId TargetTrackId { get; }

    public TrackRelationType RelationType { get; }

    public static TrackRelation Create(
        TrackRelationId id,
        TrackId sourceTrackId,
        TrackId targetTrackId,
        TrackRelationType type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return sourceTrackId == targetTrackId
            ? throw new DomainException("track_relation.self_relation", "Track relation cannot reference the same track twice")
            : new TrackRelation(id, sourceTrackId, targetTrackId, type);
    }
}

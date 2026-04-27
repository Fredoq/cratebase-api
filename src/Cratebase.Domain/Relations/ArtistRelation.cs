using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Optional;

namespace Cratebase.Domain.Relations;

public sealed class ArtistRelation : IEntity<ArtistRelationId>
{
    private ArtistRelation(
        ArtistRelationId id,
        ArtistId sourceArtistId,
        ArtistId targetArtistId,
        ArtistRelationType type,
        OptionalValue<ArtistRelationPeriod> period)
    {
        Id = id;
        SourceArtistId = sourceArtistId;
        TargetArtistId = targetArtistId;
        Type = type;
        Period = period;
    }

    public ArtistRelationId Id { get; }

    public ArtistId SourceArtistId { get; }

    public ArtistId TargetArtistId { get; }

    public ArtistRelationType Type { get; }

    public OptionalValue<ArtistRelationPeriod> Period { get; }

    public static ArtistRelation Create(
        ArtistRelationId id,
        ArtistId sourceArtistId,
        ArtistId targetArtistId,
        ArtistRelationType type)
    {
        return sourceArtistId == targetArtistId
            ? throw new DomainException("artist_relation.self_relation", "Artist relation cannot reference the same artist twice")
            : new ArtistRelation(id, sourceArtistId, targetArtistId, type, Optional.Missing<ArtistRelationPeriod>());
    }

    public static ArtistRelation Create(
        ArtistRelationId id,
        ArtistId sourceArtistId,
        ArtistId targetArtistId,
        ArtistRelationType type,
        ArtistRelationPeriod period)
    {
        ArgumentNullException.ThrowIfNull(period);

        return sourceArtistId == targetArtistId
            ? throw new DomainException("artist_relation.self_relation", "Artist relation cannot reference the same artist twice")
            : new ArtistRelation(id, sourceArtistId, targetArtistId, type, Optional.From(period));
    }
}

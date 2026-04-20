using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;

namespace Cratebase.Domain.Relations;

public sealed class ArtistRelation : IEntity<ArtistRelationId>
{
    public required ArtistRelationId Id { get; init; }

    public required ArtistId SourceArtistId { get; init; }

    public required ArtistId TargetArtistId { get; init; }

    public required ArtistRelationType Type { get; init; }

    public ArtistRelationPeriod? Period { get; init; }

    public static ArtistRelation Create(
        ArtistRelationId id,
        ArtistId sourceArtistId,
        ArtistId targetArtistId,
        ArtistRelationType type,
        ArtistRelationPeriod? period = null)
    {
        ArgumentNullException.ThrowIfNull(type);

        return sourceArtistId == targetArtistId
            ? throw new DomainException("artist_relation.self_relation", "Artist relation cannot reference the same artist twice")
            : new ArtistRelation
            {
                Id = id,
                SourceArtistId = sourceArtistId,
                TargetArtistId = targetArtistId,
                Type = type,
                Period = period
            };
    }
}

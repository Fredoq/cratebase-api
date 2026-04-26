using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public abstract class Artist(ArtistId id, string name) : IEntity<ArtistId>, INamedEntity
{
    public ArtistId Id { get; } = id;

    public string Name { get; } = Guard.RequiredText(name, nameof(name), "artist.name_required");
}

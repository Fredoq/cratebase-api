using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public abstract class Artist : IEntity<ArtistId>, INamedEntity
{
    protected Artist(ArtistId id, string name)
    {
        Id = id;
        Name = Guard.RequiredText(name, nameof(name), "artist.name_required");
    }

    public ArtistId Id { get; }

    public string Name { get; }
}

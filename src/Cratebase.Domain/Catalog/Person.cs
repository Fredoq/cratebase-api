using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Person : IEntity<PersonId>, ICreditContributor
{
    private Person(PersonId id, ArtistId artistId, string name)
    {
        Id = id;
        ArtistId = artistId;
        Name = name;
    }

    public PersonId Id { get; }

    public ArtistId ArtistId { get; }

    public string Name { get; }

    public static Person Create(PersonId id, string name)
    {
        return Create(id, ArtistId.New(), name);
    }

    public static Person Create(PersonId id, ArtistId artistId, string name)
    {
        return new Person(id, artistId, Guard.RequiredText(name, nameof(name), "person.name_required"));
    }
}

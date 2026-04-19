using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Person : IEntity<PersonId>, ICreditContributor
{
    public required PersonId Id { get; init; }

    public required ArtistId ArtistId { get; init; }

    public required string Name { get; init; }

    public static Person Create(PersonId id, string name)
    {
        return new Person
        {
            Id = id,
            ArtistId = ArtistId.New(),
            Name = Guard.RequiredText(name, nameof(name), "person.name_required")
        };
    }
}

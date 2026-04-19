using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Group : IEntity<GroupId>, ICreditContributor
{
    public required GroupId Id { get; init; }

    public required ArtistId ArtistId { get; init; }

    public required string Name { get; init; }

    public static Group Create(GroupId id, string name)
    {
        return new Group
        {
            Id = id,
            ArtistId = ArtistId.New(),
            Name = Guard.RequiredText(name, nameof(name), "group.name_required")
        };
    }
}

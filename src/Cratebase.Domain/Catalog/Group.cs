using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Group : IEntity<GroupId>, ICreditContributor
{
    private Group(GroupId id, ArtistId artistId, string name)
    {
        Id = id;
        ArtistId = artistId;
        Name = name;
    }

    public GroupId Id { get; }

    public ArtistId ArtistId { get; }

    public string Name { get; }

    public static Group Create(GroupId id, string name)
    {
        return Create(id, ArtistId.New(), name);
    }

    public static Group Create(GroupId id, ArtistId artistId, string name)
    {
        return new Group(id, artistId, Guard.RequiredText(name, nameof(name), "group.name_required"));
    }
}

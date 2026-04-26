using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Catalog;

public sealed class Group : Artist
{
    private Group(ArtistId id, string name)
        : base(id, name)
    {
    }

    public static Group Create(ArtistId id, string name)
    {
        return new Group(id, name);
    }
}

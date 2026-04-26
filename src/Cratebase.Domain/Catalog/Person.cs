using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Catalog;

public sealed class Person : Artist
{
    private Person(ArtistId id, string name)
        : base(id, name)
    {
    }

    public static Person Create(ArtistId id, string name)
    {
        return new Person(id, name);
    }
}

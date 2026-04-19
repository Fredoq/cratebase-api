using Cratebase.Domain.Shared;

namespace Cratebase.Domain.Artists;

public sealed class Artist
{
    private Artist(EntityId id, string displayName, ArtistKind kind)
    {
        Id = id;
        DisplayName = displayName;
        Kind = kind;
    }

    public EntityId Id { get; }

    public string DisplayName { get; }

    public ArtistKind Kind { get; }

    public static Artist Create(EntityId id, string displayName, ArtistKind kind)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

        string normalizedDisplayName = displayName.Trim();

        return new Artist(id, normalizedDisplayName, kind);
    }
}

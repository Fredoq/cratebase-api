using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;

namespace Cratebase.Domain.Credits;

public sealed record CreditContributor
{
    public required ArtistId ArtistId { get; init; }

    public required string Name { get; init; }

    public static CreditContributor FromArtist(IArtist artist)
    {
        ArgumentNullException.ThrowIfNull(artist);

        return new CreditContributor
        {
            ArtistId = artist.ArtistId,
            Name = artist.Name
        };
    }
}

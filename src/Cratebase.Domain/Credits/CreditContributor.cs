using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Credits;

public sealed record CreditContributor
{
    private CreditContributor(ArtistId artistId, string name)
    {
        ArtistId = artistId;
        Name = Guard.RequiredText(name, nameof(name), "credit_contributor.name_required");
    }

    public ArtistId ArtistId { get; }

    public string Name { get; }

    public static CreditContributor FromArtist(IArtist artist)
    {
        ArgumentNullException.ThrowIfNull(artist);

        return new CreditContributor(artist.ArtistId, artist.Name);
    }
}

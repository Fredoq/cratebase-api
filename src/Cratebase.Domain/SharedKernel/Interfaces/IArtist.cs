using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.SharedKernel.Interfaces;

public interface IArtist : INamedEntity
{
    ArtistId ArtistId { get; }
}

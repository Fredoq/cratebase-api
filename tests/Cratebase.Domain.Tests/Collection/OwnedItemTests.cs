using Cratebase.Domain.Collection;
using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Tests.Collection;

public sealed class OwnedItemTests
{
    [Fact]
    public void Owned_item_can_target_a_release_or_a_track_but_not_both()
    {
        var releaseItem = OwnedItem.Create(
            OwnedItemId.New(),
            OwnedItemTarget.ForRelease(ReleaseId.New()),
            OwnershipStatus.Owned,
            VinylRecord.Create("LP"));
        var trackItem = OwnedItem.Create(
            OwnedItemId.New(),
            OwnedItemTarget.ForTrack(TrackId.New()),
            OwnershipStatus.Owned,
            DigitalFile.Create(FilePath.FromAbsolutePath("/music/New Order/Blue Monday.flac"), AudioFileFormat.Flac));

        DomainException exception = Assert.Throws<DomainException>(
            () => OwnedItemTarget.Create(ReleaseId.New(), TrackId.New()));

        Assert.True(releaseItem.Target.IsRelease);
        Assert.True(trackItem.Target.IsTrack);
        Assert.Equal("owned_item_target.ambiguous", exception.Code);
    }

    [Fact]
    public void Owned_item_requires_a_concrete_medium_model()
    {
        var vinylItem = OwnedItem.Create(
            OwnedItemId.New(),
            OwnedItemTarget.ForRelease(ReleaseId.New()),
            OwnershipStatus.NeedsDigitization,
            VinylRecord.Create("12-inch"));
        var cdItem = OwnedItem.Create(
            OwnedItemId.New(),
            OwnedItemTarget.ForRelease(ReleaseId.New()),
            OwnershipStatus.Owned,
            CompactDisc.Create(1));
        var cassetteItem = OwnedItem.Create(
            OwnedItemId.New(),
            OwnedItemTarget.ForRelease(ReleaseId.New()),
            OwnershipStatus.Wanted,
            CassetteTape.Create("Chrome"));

        _ = Assert.IsType<VinylRecord>(vinylItem.Medium);
        _ = Assert.IsType<CompactDisc>(cdItem.Medium);
        _ = Assert.IsType<CassetteTape>(cassetteItem.Medium);
    }
}

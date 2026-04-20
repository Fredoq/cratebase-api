using Cratebase.Domain.Collection;
using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Tests.Collection;

public sealed class OwnedItemTests
{
    [Fact]
    public void Owned_item_can_target_a_release()
    {
        var releaseId = ReleaseId.New();
        var releaseItem = OwnedItem.Create(
            OwnedItemId.New(),
            OwnedItemTarget.ForRelease(releaseId),
            OwnershipStatus.Owned,
            VinylRecord.Create("LP"));

        Assert.True(releaseItem.Target.IsRelease);
        Assert.False(releaseItem.Target.IsTrack);
        Assert.Equal(releaseId, releaseItem.Target.ReleaseId);
        Assert.Null(releaseItem.Target.TrackId);
    }

    [Fact]
    public void Owned_item_can_target_a_track()
    {
        var trackId = TrackId.New();
        var trackItem = OwnedItem.Create(
            OwnedItemId.New(),
            OwnedItemTarget.ForTrack(trackId),
            OwnershipStatus.Owned,
            DigitalFile.Create(FilePath.FromAbsolutePath("/music/New Order/Blue Monday.flac"), AudioFileFormat.Flac));

        Assert.True(trackItem.Target.IsTrack);
        Assert.False(trackItem.Target.IsRelease);
        Assert.Equal(trackId, trackItem.Target.TrackId);
        Assert.Null(trackItem.Target.ReleaseId);
    }

    [Fact]
    public void Owned_item_target_rejects_ambiguous_targets()
    {
        DomainException exception = Assert.Throws<DomainException>(
            () => OwnedItemTarget.Create(ReleaseId.New(), TrackId.New()));

        Assert.Equal("owned_item_target.ambiguous", exception.Code);
    }

    [Fact]
    public void Owned_item_target_rejects_empty_targets()
    {
        DomainException exception = Assert.Throws<DomainException>(
            () => OwnedItemTarget.Create(null, null));

        Assert.Equal("owned_item_target.empty", exception.Code);
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

    [Fact]
    public void Concrete_media_models_validate_required_details()
    {
        Assert.Equal("vinyl_record.format_required", Assert.Throws<DomainException>(() => VinylRecord.Create(" ")).Code);
        Assert.Equal("compact_disc.disc_count_required", Assert.Throws<DomainException>(() => CompactDisc.Create(0)).Code);
        Assert.Equal("cassette_tape.type_required", Assert.Throws<DomainException>(() => CassetteTape.Create(" ")).Code);
        Assert.Equal("other_medium.name_required", Assert.Throws<DomainException>(() => OtherMedium.Create(" ")).Code);
        Assert.Equal("audio_file_format.code_required", Assert.Throws<DomainException>(() => AudioFileFormat.FromCode(" ")).Code);
    }

    [Fact]
    public void Audio_file_format_normalizes_codes_to_lowercase_and_trims_input()
    {
        var custom = AudioFileFormat.FromCode(" WAV ");

        Assert.Equal("ogg", AudioFileFormat.Ogg.Code);
        Assert.Equal("wav", custom.Code);
    }

    [Fact]
    public void File_path_accepts_unix_and_windows_absolute_paths()
    {
        var unixPath = FilePath.FromAbsolutePath("/music/New Order/Blue Monday.flac");
        var windowsPath = FilePath.FromAbsolutePath(@"C:\music\New Order\Blue Monday.flac");

        Assert.Equal("/music/New Order/Blue Monday.flac", unixPath.Value);
        Assert.Equal(@"C:\music\New Order\Blue Monday.flac", windowsPath.Value);
    }

    [Fact]
    public void File_path_requires_absolute_paths()
    {
        DomainException exception = Assert.Throws<DomainException>(() => FilePath.FromAbsolutePath("relative/file.flac"));

        Assert.Equal("file_path.not_absolute", exception.Code);
    }

    [Fact]
    public void Digital_file_requires_path_and_format()
    {
        var path = FilePath.FromAbsolutePath("/music/New Order/Blue Monday.flac");

        _ = Assert.Throws<ArgumentNullException>(() => DigitalFile.Create(null!, AudioFileFormat.Flac));
        _ = Assert.Throws<ArgumentNullException>(() => DigitalFile.Create(path, null!));
    }
}

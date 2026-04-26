using Cratebase.Domain.Catalog;
using Cratebase.Domain.Ratings;
using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Tests.Catalog;

public sealed class CatalogModelTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Named_catalog_models_reject_blank_names_and_titles(string value)
    {
        Assert.Equal("artist.name_required", Assert.Throws<DomainException>(() => Person.Create(ArtistId.New(), value)).Code);
        Assert.Equal("artist.name_required", Assert.Throws<DomainException>(() => Group.Create(ArtistId.New(), value)).Code);
        Assert.Equal("label.name_required", Assert.Throws<DomainException>(() => Label.Create(LabelId.New(), value)).Code);
        Assert.Equal("track.title_required", Assert.Throws<DomainException>(() => Track.Create(TrackId.New(), value)).Code);
        Assert.Equal("release.title_required", Assert.Throws<DomainException>(() => Release.Create(ReleaseId.New(), value)).Code);
    }

    [Fact]
    public void The_same_track_can_appear_on_multiple_releases_and_keep_one_canonical_rating()
    {
        Track track = Track.Create(TrackId.New(), "Blue Monday").WithRating(Rating.FromValue(10));
        var firstReleaseId = ReleaseId.New();
        var secondReleaseId = ReleaseId.New();
        Release firstRelease = Release.Create(firstReleaseId, "Blue Monday")
            .WithTrack(ReleaseTrack.Create(track.Id, TrackPosition.FromNumber(1)));
        Release secondRelease = Release.Create(secondReleaseId, "Substance")
            .WithTrack(ReleaseTrack.Create(track.Id, TrackPosition.FromNumber(5)));

        Assert.Equal(track.Id, firstRelease.Tracklist.Single().TrackId);
        Assert.Equal(track.Id, secondRelease.Tracklist.Single().TrackId);
        Assert.Equal(10, track.Rating?.Value);
    }

    [Fact]
    public void Track_position_rejects_non_positive_numbers_and_normalizes_markers()
    {
        DomainException exception = Assert.Throws<DomainException>(() => TrackPosition.FromNumber(0));
        var position = TrackPosition.FromNumber(1, " A ", "  ");

        Assert.Equal("track_position.number_required", exception.Code);
        Assert.Equal("A", position.Disc);
        Assert.Null(position.Side);
    }

    [Fact]
    public void Release_track_normalizes_blank_title_override()
    {
        var releaseTrack = ReleaseTrack.Create(
            TrackId.New(),
            TrackPosition.FromNumber(1),
            "   ");

        Assert.Null(releaseTrack.TitleOverride);
    }

    [Fact]
    public void Release_rating_is_independent_from_average_track_rating()
    {
        Track firstTrack = Track.Create(TrackId.New(), "Age of Consent").WithRating(Rating.FromValue(10));
        Track secondTrack = Track.Create(TrackId.New(), "We All Stand").WithRating(Rating.FromValue(8));
        var releaseId = ReleaseId.New();
        Release release = Release.Create(releaseId, "Power, Corruption & Lies")
            .WithRating(Rating.FromValue(7))
            .WithTrack(ReleaseTrack.Create(firstTrack.Id, TrackPosition.FromNumber(1)))
            .WithTrack(ReleaseTrack.Create(secondTrack.Id, TrackPosition.FromNumber(2)));

        ReleaseTrackRatingSummary summary = ReleaseTrackRatingCalculator.Calculate(release, [firstTrack, secondTrack]);

        Assert.Equal(7, release.Rating?.Value);
        Assert.Equal(9m, summary.AverageRating);
        Assert.Equal(2, summary.RatedTrackCount);
    }

    [Fact]
    public void Release_track_rating_summary_ignores_unrated_tracks_and_can_be_empty()
    {
        Track ratedTrack = Track.Create(TrackId.New(), "Leave Me Alone").WithRating(Rating.FromValue(9));
        var unratedTrack = Track.Create(TrackId.New(), "The Village");
        var releaseId = ReleaseId.New();
        Release release = Release.Create(releaseId, "Power, Corruption & Lies")
            .WithTrack(ReleaseTrack.Create(ratedTrack.Id, TrackPosition.FromNumber(8)))
            .WithTrack(ReleaseTrack.Create(unratedTrack.Id, TrackPosition.FromNumber(9)));

        ReleaseTrackRatingSummary summary = ReleaseTrackRatingCalculator.Calculate(release, [ratedTrack, unratedTrack]);
        ReleaseTrackRatingSummary emptySummary = ReleaseTrackRatingCalculator.Calculate(release, [unratedTrack]);

        Assert.Equal(9m, summary.AverageRating);
        Assert.Equal(1, summary.RatedTrackCount);
        Assert.Null(emptySummary.AverageRating);
        Assert.Equal(0, emptySummary.RatedTrackCount);
    }

    [Fact]
    public void Release_track_rating_summary_tolerates_duplicate_track_snapshots()
    {
        Track ratedTrack = Track.Create(TrackId.New(), "Ceremony").WithRating(Rating.FromValue(10));
        Track duplicateSnapshot = Track.Create(ratedTrack.Id, "Ceremony").WithRating(Rating.FromValue(8));
        var releaseId = ReleaseId.New();
        Release release = Release.Create(releaseId, "Ceremony")
            .WithTrack(ReleaseTrack.Create(ratedTrack.Id, TrackPosition.FromNumber(1)));

        ReleaseTrackRatingSummary summary = ReleaseTrackRatingCalculator.Calculate(release, [ratedTrack, duplicateSnapshot]);

        Assert.Equal(10m, summary.AverageRating);
        Assert.Equal(1, summary.RatedTrackCount);
    }

    [Fact]
    public void Release_rejects_duplicate_track_positions()
    {
        var releaseId = ReleaseId.New();
        Release release = Release.Create(releaseId, "Low-Life")
            .WithTrack(ReleaseTrack.Create(TrackId.New(), TrackPosition.FromNumber(1)));
        var duplicatePosition = ReleaseTrack.Create(TrackId.New(), TrackPosition.FromNumber(1));

        DomainException exception = Assert.Throws<DomainException>(() => release.WithTrack(duplicatePosition));

        Assert.Equal("release_track.position_duplicate", exception.Code);
    }

    [Fact]
    public void Release_can_store_type_and_cover_image()
    {
        var labelId = LabelId.New();
        var releaseDate = new DateOnly(1989, 1, 30);
        Release release = Release.Create(ReleaseId.New(), "Technique")
            .WithType(ReleaseType.Album)
            .WithLabel(labelId)
            .WithReleaseYear(1989)
            .WithReleaseDate(releaseDate)
            .WithCoverImage(CoverImage.FromPath("covers/new-order-technique.jpg"))
            .WithGenre(Genre.FromName("Alternative Dance"))
            .WithTag(Tag.FromName("favorite"));

        Assert.Equal(ReleaseType.Album, release.Type);
        Assert.Equal(labelId, release.LabelId);
        Assert.Equal(1989, release.Year);
        Assert.Equal(releaseDate, release.ReleaseDate);
        Assert.Equal("covers/new-order-technique.jpg", release.CoverImage?.Path);
        Assert.Contains(release.Genres, genre => genre.Name == "Alternative Dance");
        Assert.Contains(release.Tags, tag => tag.Name == "favorite");
    }

    [Fact]
    public void Release_type_and_cover_image_validate_required_values()
    {
        Assert.Equal("release_type.code_required", Assert.Throws<DomainException>(() => ReleaseType.FromCode(" ")).Code);
        Assert.Equal("cover_image.path_required", Assert.Throws<DomainException>(() => CoverImage.FromPath(" ")).Code);
    }

    [Fact]
    public void Track_duration_must_be_positive_when_present()
    {
        var track = Track.Create(TrackId.New(), "Dreams Never End");

        DomainException exception = Assert.Throws<DomainException>(() => track.WithDuration(TimeSpan.Zero));

        Assert.Equal("track.duration_required", exception.Code);
    }

    [Fact]
    public void Track_can_store_duration_genres_and_tags()
    {
        Track track = Track.Create(TrackId.New(), "Dreams Never End")
            .WithDuration(TimeSpan.FromMinutes(3))
            .WithGenre(Genre.FromName("Post-punk"))
            .WithTag(Tag.FromName("opener"));

        Assert.Equal(TimeSpan.FromMinutes(3), track.Duration);
        Assert.Contains(track.Genres, genre => genre.Name == "Post-punk");
        Assert.Contains(track.Tags, tag => tag.Name == "opener");
    }
}

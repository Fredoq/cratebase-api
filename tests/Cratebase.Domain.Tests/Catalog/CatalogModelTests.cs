using Cratebase.Domain.Catalog;
using Cratebase.Domain.Ratings;
using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Optional;

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
        Assert.Equal(10, Assert.IsType<PresentOptionalValue<Rating>>(track.Details.Rating).Value.Value);
    }

    [Fact]
    public void Track_position_rejects_non_positive_numbers_and_normalizes_markers()
    {
        DomainException exception = Assert.Throws<DomainException>(() => TrackPosition.FromNumber(0));
        var position = TrackPosition.FromNumber(1, " A ", "  ");

        Assert.Equal("track_position.number_required", exception.Code);
        Assert.Equal("A", Assert.IsType<PresentOptionalValue<string>>(position.Disc).Value);
        Assert.False(position.Side.HasValue);
    }

    [Fact]
    public void Release_track_normalizes_blank_title_override()
    {
        var releaseTrack = ReleaseTrack.Create(
            TrackId.New(),
            TrackPosition.FromNumber(1),
            "   ");

        Assert.False(releaseTrack.TitleOverride.HasValue);
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

        Assert.Equal(7, Assert.IsType<PresentOptionalValue<Rating>>(release.Summary.Rating).Value.Value);
        Assert.Equal(9m, Assert.IsType<PresentOptionalValue<decimal>>(summary.AverageRating).Value);
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

        Assert.Equal(9m, Assert.IsType<PresentOptionalValue<decimal>>(summary.AverageRating).Value);
        Assert.Equal(1, summary.RatedTrackCount);
        Assert.False(emptySummary.AverageRating.HasValue);
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

        Assert.Equal(10m, Assert.IsType<PresentOptionalValue<decimal>>(summary.AverageRating).Value);
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
        ReleaseMetadata metadata = ReleaseMetadata.Empty
            .WithType(ReleaseType.Album)
            .WithLabel(labelId)
            .WithReleaseYear(1989)
            .WithReleaseDate(releaseDate)
            .WithCoverImage(CoverImage.FromPath("covers/new-order-technique.jpg"));
        Cataloging cataloging = Cataloging.Empty
            .WithGenre(Genre.FromName("Alternative Dance"))
            .WithTag(Tag.FromName("favorite"));
        Release release = Release.Create(ReleaseId.New(), "Technique")
            .WithSummary(ReleaseSummary.Create("Technique").WithMetadata(metadata))
            .WithCataloging(cataloging);

        ReleaseMetadata actualMetadata = release.Summary.Metadata;

        Assert.Equal(ReleaseType.Album, actualMetadata.Type);
        Assert.Equal(labelId, Assert.IsType<PresentOptionalValue<LabelId>>(actualMetadata.LabelId).Value);
        Assert.Equal(1989, Assert.IsType<PresentOptionalValue<int>>(actualMetadata.Year).Value);
        Assert.Equal(releaseDate, Assert.IsType<PresentOptionalValue<DateOnly>>(actualMetadata.ReleaseDate).Value);
        Assert.Equal(
            "covers/new-order-technique.jpg",
            Assert.IsType<PresentOptionalValue<CoverImage>>(actualMetadata.CoverImage).Value.Path);
        Assert.Contains(release.Cataloging.Genres, genre => genre.Name == "Alternative Dance");
        Assert.Contains(release.Cataloging.Tags, tag => tag.Name == "favorite");
    }

    [Fact]
    public void Release_type_is_a_closed_object_catalog()
    {
        Assert.Equal(ReleaseType.Album, ReleaseType.Album);
        Assert.NotEqual(ReleaseType.Album, ReleaseType.Ep);
    }

    [Fact]
    public void Cover_image_validates_required_values()
    {
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
            .WithCataloging(
                Cataloging.Empty
                    .WithGenre(Genre.FromName("Post-punk"))
                    .WithTag(Tag.FromName("opener")));

        Assert.Equal(TimeSpan.FromMinutes(3), Assert.IsType<PresentOptionalValue<TimeSpan>>(track.Details.Duration).Value);
        Assert.Contains(track.Cataloging.Genres, genre => genre.Name == "Post-punk");
        Assert.Contains(track.Cataloging.Tags, tag => tag.Name == "opener");
    }
}

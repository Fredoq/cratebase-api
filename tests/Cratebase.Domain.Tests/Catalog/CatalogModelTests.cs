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
        Assert.Equal("person.name_required", Assert.Throws<DomainException>(() => Person.Create(PersonId.New(), value)).Code);
        Assert.Equal("group.name_required", Assert.Throws<DomainException>(() => Group.Create(GroupId.New(), value)).Code);
        Assert.Equal("label.name_required", Assert.Throws<DomainException>(() => Label.Create(LabelId.New(), value)).Code);
        Assert.Equal("track.title_required", Assert.Throws<DomainException>(() => Track.Create(TrackId.New(), value)).Code);
        Assert.Equal("release.title_required", Assert.Throws<DomainException>(() => Release.Create(ReleaseId.New(), value)).Code);
    }

    [Fact]
    public void The_same_track_can_appear_on_multiple_releases_and_keep_one_canonical_rating()
    {
        Track track = Track.Create(TrackId.New(), "Blue Monday").WithRating(Rating.FromValue(10));
        Release firstRelease = Release.Create(ReleaseId.New(), "Blue Monday")
            .WithTrack(ReleaseTrack.Create(ReleaseId.New(), track.Id, TrackPosition.FromNumber(1)));
        Release secondRelease = Release.Create(ReleaseId.New(), "Substance")
            .WithTrack(ReleaseTrack.Create(ReleaseId.New(), track.Id, TrackPosition.FromNumber(5)));

        Assert.Equal(track.Id, firstRelease.Tracklist.Single().TrackId);
        Assert.Equal(track.Id, secondRelease.Tracklist.Single().TrackId);
        Assert.Equal(10, track.Rating?.Value);
    }

    [Fact]
    public void Release_rating_is_independent_from_average_track_rating()
    {
        Track firstTrack = Track.Create(TrackId.New(), "Age of Consent").WithRating(Rating.FromValue(10));
        Track secondTrack = Track.Create(TrackId.New(), "We All Stand").WithRating(Rating.FromValue(8));
        Release release = Release.Create(ReleaseId.New(), "Power, Corruption & Lies")
            .WithRating(Rating.FromValue(7))
            .WithTrack(ReleaseTrack.Create(ReleaseId.New(), firstTrack.Id, TrackPosition.FromNumber(1)))
            .WithTrack(ReleaseTrack.Create(ReleaseId.New(), secondTrack.Id, TrackPosition.FromNumber(2)));

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
        Release release = Release.Create(ReleaseId.New(), "Power, Corruption & Lies")
            .WithTrack(ReleaseTrack.Create(ReleaseId.New(), ratedTrack.Id, TrackPosition.FromNumber(8)))
            .WithTrack(ReleaseTrack.Create(ReleaseId.New(), unratedTrack.Id, TrackPosition.FromNumber(9)));

        ReleaseTrackRatingSummary summary = ReleaseTrackRatingCalculator.Calculate(release, [ratedTrack, unratedTrack]);
        ReleaseTrackRatingSummary emptySummary = ReleaseTrackRatingCalculator.Calculate(release, [unratedTrack]);

        Assert.Equal(9m, summary.AverageRating);
        Assert.Equal(1, summary.RatedTrackCount);
        Assert.Null(emptySummary.AverageRating);
        Assert.Equal(0, emptySummary.RatedTrackCount);
    }
}
